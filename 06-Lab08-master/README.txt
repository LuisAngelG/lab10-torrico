# Stored Procedures

--Listar
CREATE PROCEDURE ListCustomers
AS
BEGIN
    SELECT * FROM customers
END


--Insertar
CREATE PROCEDURE InsertCustomer
    @name NVARCHAR(50),
    @address NVARCHAR(50),
    @phone NVARCHAR(10)
AS
BEGIN
    INSERT INTO customers (name, address, phone)
    VALUES (@name, @address, @phone)
END


--Actualizar
CREATE PROCEDURE UpdateCustomer
    @name NVARCHAR(50),
    @address NVARCHAR(50),
    @phone NVARCHAR(10)
AS
BEGIN
    UPDATE customers
    SET 
        name = @name,
        address = @address,
        phone = @phone
    WHERE customer_id=@customer_id;
END


--Eliminar
CREATE PROC DeleteCustomer
@customer_id int
AS
BEGIN
    UPDATE customers
    SET active=0
    WHERE customer_id=@customer_id
END
