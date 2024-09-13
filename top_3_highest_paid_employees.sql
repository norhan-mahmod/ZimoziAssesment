WITH RankedEmployees AS (
    SELECT 
        EmployeeID, 
        Name, 
        Department, 
        Salary,
        ROW_NUMBER() OVER (PARTITION BY Department ORDER BY Salary DESC) AS Rank
    FROM Employees
)
SELECT 
    EmployeeID, 
    Name, 
    Department, 
    Salary
FROM RankedEmployees
WHERE Rank <= 3;
