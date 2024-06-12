--View
CREATE VIEW AvailableMedicinesInWarehouse AS
    SELECT m.MedicineId, m.Name, m.TradeName, whm.WarehouseId, whm.Quantity
    FROM Medicine m
    JOIN WarehouseHasMedicines whm ON m.MedicineId = whm.MedicineId;

SELECT * FROM AvailableMedicinesInWarehouse

CREATE VIEW MedicineWithManufacturer AS
    SELECT m.MedicineId, m.Name, m.TradeName, m.Price, mf.Title as "Manufacturer"
    FROM Medicine m
    JOIN Manufacturer mf ON m.ManufacturerId = mf.ManufacturerId;

SELECT * FROM MedicineWithManufacturer

--Stored Procedures

CREATE PROCEDURE UpdateMedicinePrice
    @MedicineId INT,
    @NewPrice DECIMAL(13, 2)
AS
BEGIN
    UPDATE Medicine
    SET Price = @NewPrice
    WHERE MedicineId = @MedicineId;
END;

EXEC UpdateMedicinePrice 1, 13.22;

CREATE PROCEDURE UpdateWarehouseMedicineQuantity
    @MedicineId INT,
    @WarehouseId INT,
    @NewQuantity INT
AS
BEGIN
    UPDATE WarehouseHasMedicines
    SET Quantity = @NewQuantity
    WHERE MedicineId = @MedicineId
    AND WarehouseId = @WarehouseId;
END;

EXEC UpdateWarehouseMedicineQuantity 1, 2, 50;

-- Functions

--Скалярная функция для получения общей стоимости медикаментов в заявке:
CREATE FUNCTION dbo.CalculateTotalMedicineCost (@IssueRequestId INT)
RETURNS DECIMAL(13, 2)
AS
BEGIN
    DECLARE @TotalCost DECIMAL(13, 2);

    SELECT @TotalCost = SUM(m.Price * irm.Quantity)
    FROM Medicine m
    JOIN IssueRequestHasMedicine irm ON m.MedicineId = irm.MedicineId
    WHERE irm.IssueRequestId = @IssueRequestId;

    RETURN @TotalCost;
END;

-- Вычисление общей стоимости медикаментов в заявке с ID = 1
DECLARE @IssueRequestId INT = 1;
SELECT dbo.CalculateTotalMedicineCost(@IssueRequestId) AS TotalCost;

--Табличная функция GetAvailableMedicinesInWarehouse возвращает информацию о доступных лекарствах в указанном складе.
CREATE FUNCTION GetAvailableMedicinesInWarehouse (@WarehouseId int)
RETURNS TABLE
AS
RETURN (
    SELECT m.MedicineId, m.Name, m.TradeName, wm.Quantity
    FROM Medicine m
    INNER JOIN WarehouseHasMedicines wm ON m.MedicineId = wm.MedicineId
    WHERE wm.WarehouseId = @WarehouseId
);

-- Получить доступные лекарства в складе с WarehouseId = 1
SELECT * FROM dbo.GetAvailableMedicinesInWarehouse(1);

--Triggers

CREATE TABLE OpenrationLog (
	Operation VARCHAR(50), 
	ChangedDate DATETIME DEFAULT GETDATE(), 
	CurrentUser VARCHAR(50) 
);

--триггер для логирования операций 
CREATE TRIGGER trAddLogs
ON Medicine
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
	DECLARE @operation VARCHAR(10)
	IF EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
		SET @operation = 'Update'
	ELSE IF EXISTS(SELECT * FROM inserted)
		SET @operation = 'Insert'
	ELSE SET @operation = 'Delete'
	INSERT INTO OperationLog (Operation, ChangedDate, CurrentUser)
	VALUES (@operation, GETDATE(), SUSER_NAME());
END;

UPDATE Medicine
SET Price = 99.99
WHERE MedicineId = 1;

-- Создаем таблицу для истории изменений цены
CREATE TABLE PriceChangeHistory(
    MedicineId INT,
    OldPrice DECIMAL(13, 2),
    ChangeDate DATETIME DEFAULT GETDATE()
);

--Этот триггер "INSTEAD OF UPDATE" для таблицы Medicine перехватывает операции обновления 
--и вместо обновления цены записывает предыдущую цену в таблицу истории изменений цены.
--При изменении цены записывается старая цена в таблицу истории, после чего происходит фактическое обновление цены в основной таблице.

-- Создаем триггер для записи истории изменений цены
CREATE TRIGGER trg_InsteadOf_Update_Medicine
ON Medicine
INSTEAD OF UPDATE
AS
BEGIN
    IF UPDATE(Price)
    BEGIN
        DECLARE @MedicineId INT, @OldPrice DECIMAL(13, 2);

        SELECT @MedicineId = MedicineId, @OldPrice = Price
        FROM deleted;

        INSERT INTO PriceChangeHistory (MedicineId, OldPrice)
        VALUES (@MedicineId, @OldPrice);

        UPDATE M
        SET M.Price = I.Price
        FROM Medicine M
        JOIN inserted I ON M.MedicineId = I.MedicineId;
        
        PRINT 'Trigger: Instead Of UPDATE operation on Medicine table. Price change history recorded.';
    END;
END;

UPDATE Medicine
SET Price = 99.99
WHERE MedicineId = 1;
