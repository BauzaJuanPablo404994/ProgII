
----------------------------------CONSULTAR ARTICULOS------------------------------------
create procedure SP_CONSULTAR_ARTICULOS
AS
BEGIN
select * from ARTICULOS;
END

----------------------------------PROXIMO ID------------------------------------
create procedure SP_PROXIMO_ID
@proximo int OUTPUT
AS
BEGIN
SET @proximo = (
select MAX (nroFactura) + 1
from FACTURAS
);
END

----------------------------------INSERTAR DETALLE------------------------------------
create procedure SP_INSERTAR_DETALLE
@nroFactura int,
@nroDetalle int,
@codArticulo int,
@precio float,
@cantidad int
AS
BEGIN
INSERT INTO DETALLES_FACTURAS (nroFactura, nroDetalle, codArticulo, precio, cantidad)
VALUES (@nroFactura, @nroDetalle, @codArticulo, @precio, @cantidad)
END;


----------------------------------INSERTAR FACTURAS------------------------------------
create procedure SP_INSERTAR_FACTURA
@nroFactura int OUTPUT,
@nroCliente int,
@codFormaPago int,
@descuento float
AS
BEGIN
INSERT INTO FACTURAS (fecha, nroCliente, codFormaPago, descuento)
VALUES (GETDATE(), @nroCliente, @codFormaPago, @descuento)

SET @nroFactura = SCOPE_IDENTITY();
END

