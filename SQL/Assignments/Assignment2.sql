create database Assignments_db

CREATE TABLE DEPT (
    DEPTNO INT PRIMARY KEY,
    DNAME VARCHAR(50),
    LOC VARCHAR(50)
);


CREATE TABLE EMP (
    EMPNO INT PRIMARY KEY,
    ENAME VARCHAR(50),
    JOB VARCHAR(50),
    MGR_ID INT,
    HIREDATE DATE,
    SAL DECIMAL(10, 2),
    COMM DECIMAL(10, 2),
    DEPTNO INT,
    FOREIGN KEY (DEPTNO) REFERENCES DEPT(DEPTNO),
    FOREIGN KEY (MGR_ID) REFERENCES EMP(EMPNO)
);
INSERT INTO DEPT (DEPTNO, DNAME, LOC)
VALUES 
(10, 'ACCOUNTING', 'NEW YORK'),
(20, 'RESEARCH', 'DALLAS'),
(30, 'SALES', 'CHICAGO'),
(40, 'OPERATIONS', 'BOSTON');


INSERT INTO EMP (EMPNO, ENAME, JOB, MGR_ID, HIREDATE, SAL, COMM, DEPTNO)
VALUES 
(7369, 'SMITH',  'CLERK',     7902, '1980-12-17',  800,  NULL, 20),
(7499, 'ALLEN',  'SALESMAN',  7698, '1981-02-20', 1600,   300, 30),
(7521, 'WARD',   'SALESMAN',  7698, '1981-02-22', 1250,   500, 30),
(7566, 'JONES',  'MANAGER',   7839, '1981-04-02', 2975,  NULL, 20),
(7654, 'MARTIN', 'SALESMAN',  7698, '1981-09-28', 1250,  1400, 30),
(7698, 'BLAKE',  'MANAGER',   7839, '1981-05-01', 2850,  NULL, 30),
(7782, 'CLARK',  'MANAGER',   7839, '1981-06-09', 2450,  NULL, 10),
(7788, 'SCOTT',  'ANALYST',   7566, '1987-04-19', 3000,  NULL, 20),
(7839, 'KING',   'PRESIDENT', NULL, '1981-11-17', 5000,  NULL, 10),
(7844, 'TURNER', 'SALESMAN',  7698, '1981-09-08', 1500,     0, 30),
(7876, 'ADAMS',  'CLERK',     7788, '1987-05-23', 1100,  NULL, 20),
(7900, 'JAMES',  'CLERK',     7698, '1981-12-03',  950,  NULL, 30),
(7902, 'FORD',   'ANALYST',   7566, '1981-12-03', 3000,  NULL, 20),
(7934, 'MILLER', 'CLERK',     7782, '1982-01-23', 1300,  NULL, 10);
select * from emp as em with(nolock)
select * from dept as dt with(nolock)
--1. List all employees whose name begins with 'A'. 
select * from emp where ename like 'A%'
--2. Select all those employees who don't have a manager.
select * from emp where mgr_id is null
--3. List employee name, number and salary for those employees who earn in the range 1200 to 1400. 
select ename,empno,sal from emp where sal between 1200 and 1400
--4. Give all the employees in the RESEARCH department a 10% pay rise. Verify that this has been done by listing all their details before and after the rise
update emp set sal = sal *1.10 where deptno = (select deptno from dept where dname = 'research')
select * from emp where deptno = 20 

--5. Find the number of CLERKS employed. Give it a descriptive heading. 
select count(*) as "no_of_clerks" from emp where job = 'clerk'
--6. Find the average salary for each job type and the number of people employed in each job. 
select job,count(*) as "no_of_emp", AVG(Sal) as "average salary" from emp
group by job
--7. List the employees with the lowest and highest salary. 
select ename,sal from emp where sal in
                                  ((select min(sal) from emp), (select max(sal) from emp))
select * from dept
select * from emp
--8. List full details of departments that don't have any employees.
select d.deptno,d.dname from dept d 
left join 
emp e on d.deptno = e.deptno 
where e.empno is null
--9. Get the names and salaries of all the analysts earning more than 1200 who are based in department 20. Sort the answer by ascending order of name. 
select ename,sal,deptno from emp 
where job  = 'Analyst' and sal >1200 and deptno = 20 
order by ename asc 
--10. For each department, list its name and number together with the total salary paid to employees in that department. 
select d.dname ,d.deptno,sum (e.sal) from dept d 
left join emp e on d.deptno = e.deptno
group by d.deptno,d.dname
--11.  Find out salary of both MILLER and SMITH.
select ename,sal from emp where ename in ('miller','smith')
--12. Find out the names of the employees whose name begin with ‘A’ or ‘M’. 
select ename from emp  where ename like'[am]%'
--13. Compute yearly salary of SMITH. 
select ename,sal,(sal*12) as 'Annual salary'  from emp where ename = 'Smith'
--14. List the name and salary for all employees whose salary is not in the range of 1500 and 2850. 
select ename,sal from emp where sal not between 1500 and 2850
--15. Find all managers who have more than 2 employees reporting to them
select m.ename ,m.empno ,count(e.empno) as 'no of employees'
from emp e
join emp m on e.MGR_ID=m.EMPNO
group by m.ename,m.empno
having count(e.empno)>2
