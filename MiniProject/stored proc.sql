---Stored Procedure---
---stored proc registering user----
create procedure sp_register_user
    @mailid nvarchar(100),
    @passwordhash nvarchar(200),
    @salt nvarchar(100),
    @username nvarchar(200),
    @phone nvarchar(20)
as
begin
    if exists (select * from users where mailid = @mailid and isdeleted = 0)
    begin
        select 'user already exists' as message;
        return;
    end

    insert into users ( passwordhash, salt, username, phone)
    values (@mailid, @passwordhash, @salt, @username, @phone);

    select 'registration successful' as message;
end
go
---store dproc for user authentication----
create or alter proc sp_authenticate_user
    @mailid nvarchar(100),
    @passwordhash nvarchar(200)
as
begin
    set nocount on;

    select 
        userid,
        mailid,
        username,
        phone,
        createdat
    from users
    where mailid = @mailid
      and passwordhash = @passwordhash
      and isdeleted = 0;
end;

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
    set nocount on;
	
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

        -- Check availability
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

        -- Calculate total cost
        set @totalcost = @costperseat * @seatsbooked;

        -- Insert reservation
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

        -- fetch booking details
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

        -- calculate days before travel
        set @daysbefore = datediff(day, getdate(), @traveldate);

        -- determine refund percentage
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

        -- calculate refund amount
        set @refundamount = @totalcost * @refundpercent;

        -- insert cancellation record
        insert into cancellations (bookingid, seatscancelled, refundamount)
        values (@bookingid, @seatsbooked, @refundamount);

        -- update reservation status
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
USE RailwayReservationDB;
GO

CREATE or alter PROC sp_get_available_trains
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




