select * from emp
select * from dept
--1. Retrieve a list of MANAGERS.
select empno,ename,job from emp where job = 'Manager'
--Find out the names and salaries of all employees earning more than 1000 per month
select ename,sal from emp where sal >1000
--3. Display the names and salaries of all employees except JAMES. 
select ename,sal from emp where ename != 'James'
--4. Find out the details of employees whose names begin with ‘S’. 
select ename from emp where ename like 'S%'
--5. Find out the names of all employees that have ‘A’ anywhere in their name.
select ename from emp  where ename like '%A%'
--6. Find out the names of all employees that have ‘L’ as their third character in their name. 
SELECT ENAME FROM EMP WHERE ENAME LIKE '__L%'
--7. Compute daily salary of JONES. 
select ename ,sal ,(sal/30) as 'daily salary' from emp where ename = 'jones'
--8. Calculate the total monthly salary of all employees. 
select sum(sal) as 'total emp salary' from emp
--9. Print the average annual salary . 
select avg(sal  *12) as 'avg annual salary' from emp
--10. Select the name, job, salary, department number of all employees except SALESMAN from department number 30.
select ename,job,sal,deptno from emp where deptno = 30 and job!='salesman'
--11. List unique departments of the EMP table. 
select distinct deptno from emp
--12. List the name and salary of employees who earn more than 1500 and are in department 10 or 30. Label the columns Employee and Monthly Salary respectively.
select ename,sal from emp where sal > 1500 and DEPTNO in (10,30)
--13. Display the name, job, and salary of all the employees whose job is MANAGER or ANALYST and their salary is not equal to 1000, 3000, or 5000. 
select ename,job,sal from emp where job in ('manager','analyst') and sal not in (1000,3000,5000)
--14. Display the name, salary and commission for all employees whose commission amount is greater than their salary increased by 10%. 
select ename,sal,comm from emp where comm>sal*1.1
--15. Display the name of all employees who have two Ls in their name and are in department 30 or their manager is 7782.
SELECT ename from emp where (ename LIKE '%L%L%' and deptno = 30) or mgr_id = 7782;
--16. Display the names of employees with experience of over 30 years and under 40 yrs.Count the total number of employees. 
SELECT ename, hiredate,FLOOR(datediff(day, CAST(hiredate AS DATE), GETDATE()) / 365.25) AS experience_years FROM emp
WHERE FLOOR(datediff(day, CAST(hiredate AS DATE), GETDATE()) / 365.25) BETWEEN 31 AND 39;
--17. Retrieve the names of departments in ascending order and their employees in descending order. 
select e.ename,d.deptno,d.dname from emp e join dept d on e.deptno = d.deptno 
order by d.dname  asc ,e.ename  desc
--18. Find out experience of MILLER. 
SELECT ename,floor(datediff(day, convert(date, hiredate, 106), getdate()) / 365.25) AS experience_years
FROM emp
WHERE ename = 'MILLER';




