CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY,
    CategoryName NVARCHAR(100)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100),
    Price SMALLMONEY,
    CategoryID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Ingredients (
    IngredientID INT PRIMARY KEY,
    Name NVARCHAR(100),
    RecentAmount DECIMAL(10, 2)
);

CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(100),
    MidName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100),
    Position NVARCHAR(50),
    Salary SMALLMONEY
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY,
    UserName NVARCHAR(100),
    Password NVARCHAR(100),
    Permission NVARCHAR(50),
    EmployeeID INT,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE Phones (
    PhonesID INT PRIMARY KEY,
    phoneNumber NVARCHAR(20),
    EmployeeID INT,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

CREATE TABLE CustomerID (
    CustomerID INT PRIMARY KEY,
    FirstName NVARCHAR(100),
    MidName NVARCHAR(100),
    LastName NVARCHAR(100),
    Email NVARCHAR(100),
    Address NVARCHAR(255)
);

CREATE TABLE PhonesCustomer (
    PhonesID INT PRIMARY KEY,
    phoneNumber NVARCHAR(20),
    CustomerID INT,
    FOREIGN KEY (CustomerID) REFERENCES CustomerID(CustomerID)
);

CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    OrderDate DATE,
    TotalPrice SMALLMONEY,
    EmployeeID INT,
    CustomerID INT,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    FOREIGN KEY (CustomerID) REFERENCES CustomerID(CustomerID)
);

CREATE TABLE OrderProducts (
    OrderID INT,
    ProductID INT,
    TotalAmount INT,
    TotalPrice SMALLMONEY,
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);

CREATE TABLE ProductIngredients (
    ProductID INT,
    IngredientID INT,
    TotalAmountConsumed DECIMAL(10, 2),
    TheDateConsumed DATE,
    PRIMARY KEY (ProductID, IngredientID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (IngredientID) REFERENCES Ingredients(IngredientID)
);

CREATE TABLE Purchases (
    PurchaseID INT PRIMARY KEY,
    BuyDate DATE,
    TotalPrice SMALLMONEY
);

CREATE TABLE PurchaseIngredients (
    PurchaseID INT,
    IngredientID INT,
    TotalAmount DECIMAL(10, 2),
    TotalPrice SMALLMONEY,
    PriceForEachUnit SMALLMONEY,
    PRIMARY KEY (PurchaseID, IngredientID),
    FOREIGN KEY (PurchaseID) REFERENCES Purchases(PurchaseID),
    FOREIGN KEY (IngredientID) REFERENCES Ingredients(IngredientID)
);
