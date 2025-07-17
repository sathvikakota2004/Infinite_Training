create table clients (
    client_id int primary key,
    cname varchar(40) not null,
    address varchar(30),
    email varchar(30) unique,
    phone bigint,
    business varchar(20) not null
);

create table departments (
    deptno int primary key,
    dname varchar(15) not null,
    loc varchar(20)
);

create table employees (
    empno int primary key,
    ename varchar(20) not null,
    job varchar(15),
    salary int check (salary > 0),
    deptno int references departments(deptno)
    
);

create table projects (
    project_id int primary key,
    descr varchar(30) not null,
    start_date date,
    planned_end_date date,
    actual_end_date date,
    budget int check (budget > 0),
    client_id int references clients(client_id)
);


create table empprojecttasks (
    project_id int,
    empno int,
    start_date date,
    end_date date,
    task varchar(25) not null,
    status varchar(15) not null,
    primary key (project_id, empno),
    foreign key (project_id) references projects(project_id),
    foreign key (empno) references employees(empno)
);

insert into clients  values
(1001, 'acme utilities', 'noida', 'contact@acmeutil.com', 9567880032, 'manufacturing'),
(1002, 'trackon consultants', 'mumbai', 'consult@trackon.com', 8734210090, 'consultant'),
(1003, 'moneysaver distributors', 'kolkata', 'save@moneysaver.com', 7799886655, 'reseller'),
(1004, 'lawful corp', 'chennai', 'justice@lawful.com', 9210342219, 'professional');


insert into departments values
(10, 'design', 'pune'),
(20, 'development', 'pune'),
(30, 'testing', 'mumbai'),
(40, 'document', 'mumbai');


insert into employees  values
(7001, 'sandeep', 'analyst', 25000, 10),
(7002, 'rajesh', 'designer', 30000, 10),
(7003, 'madhav', 'developer', 40000, 20),
(7004, 'manoj', 'developer', 40000, 20),
(7005, 'abhay', 'designer', 35000, 10),
(7006, 'uma', 'tester', 30000, 30),
(7007, 'gita', 'tech. writer', 30000, 40),
(7008, 'priya', 'tester', 35000, 30),
(7009, 'nutan', 'developer', 45000, 20),
(7010, 'smita', 'analyst', 20000, 10),
(7011, 'anand', 'project mgr', 65000, 10);


insert into projects  values
(401, 'inventory', '2011-04-01', '2011-10-01', '2011-10-31', 150000, 1001),
(402, 'accounting', '2011-08-01', '2012-01-01', null, 500000, 1002),
(403, 'payroll', '2011-10-01', '2011-12-31', null, 75000, 1003),
(404, 'contact mgmt', '2011-11-01', '2011-12-31', null, 50000, 1004);



insert into empprojecttasks values
(401, 7001, '2011-04-01', '2011-04-20', 'system analysis', 'completed'),
(401, 7002, '2011-04-21', '2011-05-30', 'system design', 'completed'),
(401, 7003, '2011-06-01', '2011-07-15', 'coding', 'completed'),
(401, 7004, '2011-07-18', '2011-09-01', 'coding', 'completed'),
(401, 7006, '2011-09-03', '2011-09-15', 'testing', 'completed'),
(401, 7009, '2011-09-18', '2011-10-05', 'code change', 'completed'),
(401, 7008, '2011-10-06', '2011-10-16', 'testing', 'completed'),
(401, 7007, '2011-10-06', '2011-10-22', 'documentation', 'completed'),
(401, 7011, '2011-10-22', '2011-10-31', 'sign off', 'completed'),
(402, 7010, '2011-08-01', '2011-08-20', 'system analysis', 'completed'),
(402, 7002, '2011-08-22', '2011-09-30', 'system design', 'completed'),
(402, 7004, '2011-10-01', null, 'coding', 'in progress');


select * from clients as ct with(nolock);
select * from departments as dt with(nolock);
select * from employees as et with(nolock);
select * from projects as pt with(nolock);
select * from empprojecttasks as ep with(nolock);
