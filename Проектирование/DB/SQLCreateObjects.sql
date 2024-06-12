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

--��������� ������� ��� ��������� ����� ��������� ������������ � ������:
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

-- ���������� ����� ��������� ������������ � ������ � ID = 1
DECLARE @IssueRequestId INT = 1;
SELECT dbo.CalculateTotalMedicineCost(@IssueRequestId) AS TotalCost;

--��������� ������� GetAvailableMedicinesInWarehouse ���������� ���������� � ��������� ���������� � ��������� ������.
CREATE FUNCTION GetAvailableMedicinesInWarehouse (@WarehouseId int)
RETURNS TABLE
AS
RETURN (
    SELECT m.MedicineId, m.Name, m.TradeName, wm.Quantity
    FROM Medicine m
    INNER JOIN WarehouseHasMedicines wm ON m.MedicineId = wm.MedicineId
    WHERE wm.WarehouseId = @WarehouseId
);

-- �������� ��������� ��������� � ������ � WarehouseId = 1
SELECT * FROM dbo.GetAvailableMedicinesInWarehouse(1);

--Triggers

--���� ������� ����������� ����� ������� ������ � ������� WarehouseHasMedicines.
--������� ���������, ���� �� ����������� ���������� ������ � ������� ����� ������� ����� ������. 
--���� ����� ������� ����� ���������� ������ ������ �������������, �������� ������� ���������� � ��������� ��������� �� ������.
--����� ������� �������� �������������� ���������� �������� � ������������� ������������� ������� ������ � ��������� �������.
CREATE TRIGGER trg_After_Insert_WarehouseHasMedicines
ON WarehouseHasMedicines
AFTER INSERT
AS
BEGIN
    DECLARE @MedicineId INT, @WarehouseId INT, @Quantity INT;

    SELECT TOP 1 @MedicineId = MedicineId, @WarehouseId = WarehouseId, @Quantity = Quantity
    FROM inserted;

    -- ���������, �������� �� ���������� ������ � ������� ����� �������
    IF @Quantity > 0
    BEGIN
        DECLARE @CurrentStock INT;

        SELECT @CurrentStock = Quantity
        FROM WarehouseHasMedicines
        WHERE MedicineId = @MedicineId AND WarehouseId = @WarehouseId;

        IF (@CurrentStock - @Quantity) < 0
        BEGIN
            RAISERROR('Not enough stock available for this product in this warehouse.', 16, 1);
            ROLLBACK TRANSACTION; -- ����� ��������
        END;
    END;
END;


-- ������� ������� ��� ������� ��������� ����
CREATE TABLE PriceChangeHistory(
    MedicineId INT,
    OldPrice DECIMAL(13, 2),
    ChangeDate DATETIME DEFAULT GETDATE()
);

--���� ������� "INSTEAD OF UPDATE" ��� ������� Medicine ������������� �������� ���������� 
--� ������ ���������� ���� ���������� ���������� ���� � ������� ������� ��������� ����.
--��� ��������� ���� ������������ ������ ���� � ������� �������, ����� ���� ���������� ����������� ���������� ���� � �������� �������.

-- ������� ������� ��� ������ ������� ��������� ����
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
