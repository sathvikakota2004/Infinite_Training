drop database Railwayreservationdb
-- 1. Create Database
CREATE DATABASE RailwayReservationDB;
GO

USE RailwayReservationDB;
GO
-- users table
create table users (
    userid         int identity(1,1) primary key,
    mailid         nvarchar(100) not null unique,
    passwordhash   nvarchar(200) not null,    -- store hashed password
    salt           nvarchar(100) not null,
    username       nvarchar(200),
    phone          nvarchar(20),
    createdat      datetime default getdate(),
	role           NVARCHAR(20) NOT NULL DEFAULT 'User',
    isdeleted      bit default 0              -- soft-delete flag
);
select * from users



-- trains table
create table trains (
    trainno        int primary key,           
    trainname      varchar(200) not null,
    source         varchar(100) not null,
    destination    varchar(100) not null,
    isdeleted      bit default 0
);
select * from trains
insert into trains values(12711,'Pinakni','Vijayawada','Chennai',0)
INSERT INTO trains VALUES 
(12712, 'Rajdhani', 'Delhi', 'Mumbai', 0),
(12713, 'Shatabdi ', 'Mumbai', 'Pune', 0),
(12714, 'Duronto ', 'Kolkata', 'Chennai', 0);


-- class types table
create table classtypes (
    classcode      varchar(10) primary key,  -- e.g. 'sl', '2a', '3a'
    classname      varchar(50) not null
);
select * from classtypes
insert into classtypes values ('2S','Seater')
INSERT INTO classtypes VALUES
('SL', 'Sleeper'),
('3A', '3rd AC'),
('2A', '2nd AC'),
('1A', '1st AC');

-- train classes table (availability and price)
create table trainclasses (
    trainno        int not null references trains(trainno),
    classcode      varchar(10) not null references classtypes(classcode),
    maxseats       int not null,              -- initial allotment
    availableseats int not null,              -- current available seats
    costperseat    decimal(10,2) not null,
    primary key (trainno, classcode)
);
insert into trainclasses values (12711,'2S',300,100,250)
INSERT INTO trainclasses VALUES
(12711, 'SL', 200, 50, 350.00),
(12711, '3A', 150, 80, 900.00),
(12711, '2A', 100, 40, 1500.00),
(12711, '1A', 50, 20, 2000.00);


INSERT INTO trainclasses VALUES
(12712, '2S', 280, 110, 260.00),
(12712, 'SL', 220, 70, 370.00),
(12712, '3A', 160, 90, 920.00),
(12712, '2A', 110, 35, 1550.00),
(12712, '1A', 55, 25, 2100.00);


INSERT INTO trainclasses VALUES
(12713, '2S', 250, 90, 240.00),
(12713, 'SL', 180, 60, 340.00),
(12713, '3A', 140, 75, 880.00),
(12713, '2A', 90, 30, 1400.00),
(12713, '1A', 45, 15, 1900.00);


INSERT INTO trainclasses VALUES
(12714, '2S', 320, 130, 270.00),
(12714, 'SL', 210, 100, 380.00),
(12714, '3A', 170, 95, 950.00),
(12714, '2A', 120, 45, 1600.00),
(12714, '1A', 60, 30, 2200.00);

-- reservations table
create table reservations (
    bookingid      int identity(1,1) primary key,
    userid         int not null references users(userid),
    passengername  varchar(200) not null,    -- passenger for this booking
    traveldate     date not null,
    trainno        int not null references trains(trainno),
    classcode      varchar(10) not null references classtypes(classcode),
    seatsbooked    int not null,
    totalcost      decimal(12,2) not null,
    bookingdate    datetime default getdate(),
    status         varchar(20) default 'confirmed', -- confirmed / cancelled
    isdeleted      bit default 0
);
select * from reservations

-- cancellations table
create table cancellations (
    cancellationid int identity(1,1) primary key,
    bookingid      int not null references reservations(bookingid),
    cancelledon    datetime default getdate(),
    seatscancelled int not null,
    refundamount   decimal(12,2) not null
);
select * from cancellations
select * from users

