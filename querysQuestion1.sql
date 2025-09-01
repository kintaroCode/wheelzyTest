-- Primero creamos la base de datos y luego las tablas con las columnas utiles para el test. 

CREATE DATABASE wheelzyDBs
GO

USE wheelzyDBs
GO

CREATE TABLE Car (
	[CarId] INT IDENTITY(1,1) PRIMARY KEY,
	[Plate] NVARCHAR(10) NOT NULL,
	[Year] INT NOT NULL,
	[Make] NVARCHAR(50) NOT NULL,
	[Model] NVARCHAR(50) NOT NULL,
	[Submodel] NVARCHAR(50),
	[status] NVARCHAR(20),
	[LocationId] INT
)
GO

CREATE TABLE Location(
    [LocationId] INT IDENTITY(1,1) PRIMARY KEY,
	[Zipcode] VARCHAR(10) NOT NULL,
	[Address] NVARCHAR(100),
	[City] NVARCHAR(50),
	[State]  NVARCHAR(50)	
) 
GO

ALTER TABLE Car 
ADD CONSTRAINT FK_Car_Location FOREIGN KEY (LocationID) REFERENCES Location(LocationID)
GO

CREATE TABLE Buyer(
	[BuyerId] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] NVARCHAR(100),
	[Calification] TINYINT,
	[LocationId] INT,
)
GO

ALTER TABLE Buyer ADD  CONSTRAINT FK_Buyer_Location FOREIGN KEY(LocationId)
REFERENCES Location(LocationId)
GO

ALTER TABLE Buyer
ADD CONSTRAINT chk_Calification_Range CHECK (Calification >= 0 AND Calification <= 100);
GO

CREATE TABLE Amount(
	[AmountId] INT IDENTITY(1,1) PRIMARY KEY,
	[BuyerId] INT,
	[LocationId] INT,
	[Amount] DECIMAL(8, 2),
)
GO

ALTER TABLE Amount ADD CONSTRAINT FK_Amount_Buyer FOREIGN KEY(BuyerId)
REFERENCES Buyer(BuyerId)
GO

ALTER TABLE Amount ADD FOREIGN KEY(LocationId)
REFERENCES Location(LocationId)
GO

CREATE TABLE StatusLog(
	[StatusId] INT IDENTITY(1,1) PRIMARY KEY,
	[BuyerId] INT NOT NULL,
	[CarId] INT  NOT NULL,
	[Status] NVARCHAR(50),
	[statusDate] DATETIME,
	[ChangeBy] NVARCHAR(50)
)

ALTER TABLE StatusLog  ADD CONSTRAINT FK_StatusLog_Buyer FOREIGN KEY(BuyerId)
REFERENCES Buyer(BuyerId)
GO

ALTER TABLE StatusLog ADD CONSTRAINT FK_StatusLog_Car FOREIGN KEY(CarId)
REFERENCES Car(CarId)
GO

  -- Poblamos las tablas para tener datos de antemano
INSERT INTO Location (Zipcode, Address, City, State) VALUES
('10001', '123 Main St', 'New York', 'NY'),
('90001', '456 Elm St', 'Los Angeles', 'CA'),
('60601', '789 Oak St', 'Chicago', 'IL'),
('77001', '101 Maple Ave', 'Houston', 'TX'),
('85001', '202 Pine St', 'Phoenix', 'AZ'),
('94101', '303 Cedar Rd', 'San Francisco', 'CA'),
('33101', '404 Birch Blvd', 'Miami', 'FL'),
('48201', '505 Spruce Ln', 'Detroit', 'MI'),
('75201', '606 Fir Ct', 'Dallas', 'TX'),
('20001', '707 Redwood Dr', 'Washington', 'DC'),
('90002', '808 Dogwood Ln', 'Los Angeles', 'CA'),
('60602', '909 Hawthorn St', 'Chicago', 'IL'),
('77002', '1010 Willow Ave', 'Houston', 'TX'),
('85002', '1111 Sequoia Rd', 'Phoenix', 'AZ'),
('94102', '1212 Magnolia St', 'San Francisco', 'CA'),
('33102', '1313 Aspen Ct', 'Miami', 'FL'),
('48202', '1414 Elmwood Ave', 'Detroit', 'MI'),
('75202', '1515 Chestnut St', 'Dallas', 'TX'),
('20002', '1616 Hickory Ln', 'Washington', 'DC'),
('10002', '1717 Walnut St', 'New York', 'NY');
GO

INSERT INTO Buyer (Name, Calification, LocationId) VALUES
('Carlos', 85, 1),
('Laura', 90, 2),
('José', 75, 3),
('Ana', 80, 4),
('Luis', 65, 5),
('María', 70, 6),
('Juan', 88, 7),
('Sofía', 92, 8),
('Pedro', 78, 9),
('Isabel', 83, 10),
('Fernando', 77, 11),
('Lucía', 85, 12),
('Antonio', 69, 13),
('Marta', 74, 14),
('Raúl', 82, 15),
('Claudia', 79, 16),
('Javier', 91, 17),
('Patricia', 87, 18),
('Ricardo', 73, 19),
('Elena', 90, 20);
GO

INSERT INTO Amount (BuyerId, LocationId, Amount) VALUES
(1, 1, 450.00),
(1, 2, 550.00),
(1, 3, 500.00),
(1, 4, 470.00),
(2, 5, 530.00),
(2, 6, 600.00),
(2, 2, 620.00),
(2, 8, 610.00),
(2, 9, 590.00),
(2, 10, 605.00),
(3, 11, 480.00),
(3, 12, 470.00),
(3, 2, 490.00),
(3, 14, 500.00),
(3, 15, 510.00),
(1, 16, 495.00),
(2, 17, 615.00),
(3, 18, 465.00),
(1, 19, 525.00),
(2, 20, 600.00);
GO

  -- usamos un trigger, el mismo se dispara cuando se agrega un nuevo auto a la tabla car (oferta) y crea un registro con el id del carro, se asigna un comprador 
  -- seleccionado por la ubicacion , el status de la oferta ('pending Acceptance' 'picked up' ) , en la tabla StatusLog
  -- 
CREATE TRIGGER trg_InsertNewCar
ON Car
AFTER INSERT
AS
BEGIN    
   INSERT INTO StatusLog(CarId,BuyerId, Status, statusDate, ChangeBy)
   SELECT CarId, 
		(SELECT TOP 1 BuyerId FROM Buyer WHERE Buyer.LocationId = LocationID ORDER BY Buyer.BuyerId),
		'Pending Acceptance',
		GETDATE(),
		'UserAdmin'
   FROM inserted
END

  -- Agregamos dos registro en la tabla car para ver el uso del trigger
INSERT INTO Car (Plate, Year, Make, Model, Submodel, LocationId)
VALUES ('ABC1234', 2018, 'Toyota', 'Corolla', 'LE', 5); 

INSERT INTO Car (Plate, Year, Make, Model, Submodel, LocationId)
VALUES ('ABC4321', 2018, 'Ford', 'Ka', 'LE', 5);

-- hacemos la solictud de informacion de status, status date, name del vendedor , monto ofrecido por la ubicacion, y demas datosd del auto
SELECT 
	Car.CarId,
	Car.Year,
	Car.Plate,
	Car.LocationID,
	Car.Make,
	Car.Model,
	Car.Submodel,
	Buyer.Name,
	Amount.Amount,
	StatusLog.Status,
	StatusLog.statusDate
	FROM Car 
INNER JOIN StatusLog ON StatusLog.CarId = Car.CarId
INNER JOIN Buyer ON Buyer.BuyerId = StatusLog.BuyerId
INNER JOIN Amount on Amount.BuyerId = Buyer.BuyerId and Amount.LocationId = Car.LocationID
