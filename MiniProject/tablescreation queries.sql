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

-- class types table
create table classtypes (
    classcode      varchar(10) primary key,  -- e.g. 'sl', '2a', '3a'
    classname      varchar(50) not null
);
insert into classtypes values ('2S','Seater')

insert into classtypes (classcode, classname) 
values ('sl','sleeper'), ('3a','3rd ac'), ('2a','2nd ac');

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

