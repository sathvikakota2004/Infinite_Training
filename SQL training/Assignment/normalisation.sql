create table client (
clientno varchar(10) primary key,
cname varchar(100));

create table property (
propertyno varchar(10) primary key,
paddress varchar(200));

create table owner (
ownerno varchar(10) primary key,
oname varchar(100));

create table rental (
clientno varchar(10),
propertyno varchar(10),
rentstart date,
rentfinish date,
rent int,
ownerno varchar(10),
primary key (clientno, propertyno, rentstart),
foreign key (clientno) references client(clientno),
foreign key (propertyno) references property(propertyno),
foreign key (ownerno) references owner(ownerno));
insert into client values
('cr76', 'john kay'),
('cr56', 'aline stewart');

insert into property values
('pg4', '6 lawrence st, glasgow'),
('pg16', '5 novar dr, glasgow'),
('pg36', '2 manor rd, glasgow');

insert into owner values
('co40', 'tina murphy'),
('co93', 'tony shaw');

insert into rental values
('cr76', 'pg4', '2000-07-01', '2001-08-31', 350, 'co40'),
('cr76', 'pg16', '2002-09-01', '2002-09-01', 450, 'co93'),
('cr56', 'pg4', '1999-09-01', '2000-06-10', 350, 'co40'),
('cr56', 'pg36', '2000-10-10', '2001-12-01', 400, 'co93'),
('cr56', 'pg16', '2002-11-01', '2003-08-01', 450, 'co93');
--query to view all data
select r.clientno,c.cname,r.propertyno,p.paddress,r.rentstart,r.rentfinish,r.rent,r.ownerno,o.oname from rental r
join client c on r.clientno = c.clientno
join property p on r.propertyno = p.propertyno
join owner o on r.ownerno = o.ownerno;


