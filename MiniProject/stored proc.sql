---Stored Procedure---
---stored proc registering user----
CREATE OR ALTER PROCEDURE sp_register_user
    @mailid NVARCHAR(100),
    @passwordhash NVARCHAR(200),
    @salt NVARCHAR(100),
    @username NVARCHAR(100),
    @phone NVARCHAR(20),
    @role NVARCHAR(20)   -- New parameter
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM users WHERE mailid = @mailid AND isdeleted = 0)
    BEGIN
        SELECT 'Email already exists.' AS message;
        RETURN;
    END

    INSERT INTO users (mailid, passwordhash, salt, username, phone, role, createdat, isdeleted)
    VALUES (@mailid, @passwordhash, @salt, @username, @phone, @role, GETDATE(), 0);

    SELECT 'Registration successful!' AS message;
END;
GO

---store dproc for user authentication----
CREATE OR ALTER PROCEDURE sp_authenticate_user
    @mailid NVARCHAR(100),
    @passwordhash NVARCHAR(200)
AS
BEGIN
   

    SELECT 
        userid,
        mailid,
        username,
        phone,
        role,         
        createdat
    FROM users
    WHERE mailid = @mailid
      AND passwordhash = @passwordhash
      AND isdeleted = 0;
END;
GO


--stored proc for reservation----
create or alter proc sp_make_reservation
    @username varchar(100),
    @passengername varchar(200),
    @traveldate date,
    @trainno int,
    @classcode varchar(10),
    @seatsbooked int
as
begin
    
	
    declare @userid int;
    declare @availableseats int;
    declare @costperseat decimal(10,2);
    declare @totalcost decimal(12,2);
	select @userid = userid from users where username = @username;

    
    if @userid is null
    begin
        raiserror('Invalid user.', 16, 1);
        return;
    end
begin try
        begin transaction;

                select @availableseats = availableseats,
               @costperseat = costperseat
        from trainclasses
        where trainno = @trainno
          and classcode = @classcode;

        if @availableseats is null
        begin
            raiserror('Invalid train number or class code.', 16, 1);
            rollback transaction;
            return;
        end

        if @availableseats < @seatsbooked
        begin
            raiserror('Not enough seats available.', 16, 1);
            rollback transaction;
            return;
        end

      
        set @totalcost = @costperseat * @seatsbooked;

        
        insert into reservations
            (userid, passengername, traveldate, trainno, classcode, seatsbooked, totalcost)
        values
            (@userid, @passengername, @traveldate, @trainno, @classcode, @seatsbooked, @totalcost);

        -- Update available seats
        update trainclasses
        set availableseats = availableseats - @seatsbooked
        where trainno = @trainno
          and classcode = @classcode;

        commit transaction;

        select 'reservation successful' as message,
               scope_identity() as bookingid,
               @totalcost as totalcost;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        select error_message() as error_message;
    end catch
end
go

--stored proc for cancellation---
create or alter proc sp_cancel_reservation
    @bookingid int
as
begin
    set nocount on;

    declare @traveldate date,
            @trainno int,
            @classcode varchar(10),
            @seatsbooked int,
            @totalcost decimal(12,2),
            @refundpercent decimal(5,2),
            @refundamount decimal(12,2),
            @daysbefore int;

    begin try
        begin transaction;

       
        select @traveldate = traveldate,
               @trainno = trainno,
               @classcode = classcode,
               @seatsbooked = seatsbooked,
               @totalcost = totalcost
        from reservations
        where bookingid = @bookingid
          and status = 'confirmed'
          and isdeleted = 0;

        if @traveldate is null
        begin
            raiserror('invalid booking id or already cancelled.', 16, 1);
            rollback transaction;
            return;
        end

        
        set @daysbefore = datediff(day, getdate(), @traveldate);

       
        if @daysbefore >= 4
            set @refundpercent = 0.80;
        else if @daysbefore = 3
            set @refundpercent = 0.75;
        else if @daysbefore = 2
            set @refundpercent = 0.50;
        else if @daysbefore = 1
            set @refundpercent = 0.25;
        else
            set @refundpercent = 0.00;

        
        set @refundamount = @totalcost * @refundpercent;

        
        insert into cancellations (bookingid, seatscancelled, refundamount)
        values (@bookingid, @seatsbooked, @refundamount);

        
        update reservations
        set status = 'cancelled'
        where bookingid = @bookingid;

        -- restore seats in trainclasses
        update trainclasses
        set availableseats = availableseats + @seatsbooked
        where trainno = @trainno
          and classcode = @classcode;

        commit transaction;

        select 'cancellation successful' as message,
               @refundamount as refund_amount,
               @refundpercent * 100 as refund_percentage;
    end try
    begin catch
        if @@trancount > 0
            rollback transaction;

        select error_message() as error_message;
    end catch
end
go
----stored proc for showing booking details---
create or alter proc sp_get_booking_details
    @bookingid int
as
begin
    

    select 
        r.bookingid,
        r.userid,
        u.username,
        u.mailid,
        r.passengername,
        r.traveldate,
        t.trainno,
        t.trainname,
        t.source,
        t.destination,
        r.classcode,
        c.classname,
        r.seatsbooked,
        r.totalcost,
        r.bookingdate,
        r.status
    from reservations r
    join users u on r.userid = u.userid
    join trains t on r.trainno = t.trainno
    join classtypes c on r.classcode = c.classcode
    where r.bookingid = @bookingid
      and r.isdeleted = 0;
end;
---show available trians---



create or alter PROC sp_get_available_trains
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        t.Trainno,
        t.Trainname,
        t.Source,
        t.Destination,
        tc.Classcode,
        tc.Maxseats,
        tc.Availableseats,
        tc.Costperseat
    FROM trains t
    INNER JOIN trainclasses tc 
        ON t.trainno = tc.trainno
    WHERE t.isdeleted = 0
      AND tc.availableseats > 0
    ORDER BY t.trainno, tc.classcode;
END
GO
----stored proc to get all bookings---
CREATE OR ALTER PROC sp_get_all_booking
AS
BEGIN
    SELECT
        tc.trainno,
        t.trainname,
        t.source,
        t.destination,
        STRING_AGG(CONCAT(tc.classcode, ':', tc.availableseats, ' seats, ₹', tc.costperseat), '; ') AS classes_info
    FROM trainclasses tc
    INNER JOIN trains t ON tc.trainno = t.trainno
    GROUP BY
        tc.trainno,
        t.trainname,
        t.source,
        t.destination
    ORDER BY tc.trainno;
END


exec sp_get_all_booking
---stored proc to get all cancellation ----
create or alter proc sp_get_all_cancelled
as
begin
select * from cancellations
end
go
exec sp_get_all_cancelled
---stored proc for adding a train---
CREATE OR ALTER PROC sp_add_train
    @trainno        INT,          
    @trainname      VARCHAR(200),
    @source         VARCHAR(100),
    @destination    VARCHAR(100),
    @classcode      VARCHAR(10),
    @maxseats       INT,
    @availableseats INT,
    @costperseat    DECIMAL(10,2)
AS
BEGIN
    

    
    IF NOT EXISTS (SELECT 1 FROM trains WHERE trainno = @trainno)
    BEGIN
        INSERT INTO trains (trainno, trainname, source, destination, isdeleted)
        VALUES (@trainno, @trainname, @source, @destination, 0);
    END

    
    INSERT INTO trainclasses (trainno, classcode, maxseats, availableseats, costperseat)
    VALUES (@trainno, @classcode, @maxseats, @availableseats, @costperseat);
END

--stored proc for updation of train details---
CREATE OR ALTER PROCEDURE sp_update_train
    @trainno        INT,          
    @trainname      VARCHAR(200),
    @source         VARCHAR(100),
    @destination    VARCHAR(100)
AS
BEGIN
    UPDATE trains
    SET 
        trainname = ISNULL(@trainname, trainname),
        source = ISNULL(@source, source),
        destination = ISNULL(@destination, destination)
    WHERE trainno = @trainno;
END
GO
--stored proc for deleting train---
CREATE OR ALTER PROC sp_delete_train
    @trainno INT
AS
BEGIN
    SET NOCOUNT ON;

    
    INSERT INTO cancellations (bookingid, cancelledon, seatscancelled,refundamount)
    SELECT 
        r.bookingid,
        GETDATE(), 
		r.seatsbooked,
        r.totalcost               
    FROM reservations r
    WHERE r.trainno = @trainno
      AND r.status = 'Confirmed';    

    
    UPDATE reservations
    SET status = 'Cancelled'
    WHERE trainno = @trainno
      AND status = 'Confirmed';

    
    DELETE FROM trainclasses
    WHERE trainno = @trainno;

    
    UPDATE trains
    SET isdeleted = 1
    WHERE trainno = @trainno;
END
GO
---store dproc to get all reservation---
create or alter proc sp_get_all_reservations
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        r.bookingid,
      
        r.passengername,
        t.trainno,
        t.trainname,
        
        c.classname AS ClassName,
        r.seatsbooked,
        r.totalcost,
       CONVERT(VARCHAR(10), r.traveldate, 120) AS TravelDate, 
       
        r.status
    FROM reservations r
    INNER JOIN users u ON r.userid = u.userid
    INNER JOIN trains t ON r.trainno = t.trainno
    INNER JOIN classtypes c ON r.classcode = c.classcode
    WHERE r.isdeleted = 0
    ORDER BY r.bookingdate ASC;
END;
GO
