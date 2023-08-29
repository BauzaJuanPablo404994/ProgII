CREATE DATABASE VENTAS_LOCAL
USE VENTAS_LOCAL


-------------------------------------DISEÑO DE LA DB------------------------------------------

CREATE TABLE FORMAS_PAGO(
codFormaPago int identity(1,1),
formaPago varchar(20)
constraint pk_FORMAS_PAGO primary key (codFormaPago)
)

CREATE TABLE ARTICULOS(
codArticulo int identity(1,1),
nombre varchar(50),
precioUnitario float,
activo bit
constraint pk_ARTICULOS primary key (codArticulo)
)

CREATE TABLE CLIENTES(
nroCliente int identity(1,1),
apellido varchar(50),
nombre varchar(50),
documento int
constraint pk_CLIENTES primary key (nroCliente)
)

CREATE TABLE FACTURAS(
nroFactura int identity(1,1),
nroCliente int,
fecha datetime,
codFormaPago int,
descuento float
constraint pk_FACTURAS primary key (nroFactura),
constraint fk_FACTURAS_CLIENTES foreign key (nroCliente)
references CLIENTES (nroCliente),
constraint fk_FACTURAS_FORMAS_PAGO foreign key (codFormaPago)
references FORMAS_PAGO(codFormaPago)
)

CREATE TABLE DETALLES_FACTURAS(
nroFactura int,
nroDetalle int,
codArticulo int,
precio float,
cantidad int
constraint pk_DETALLES_FACTURAS primary key (nroFactura, nroDetalle),
constraint fk_DETALLES_FACTURAS_a_FACTURAS foreign key (nroFactura)
references FACTURAS (nroFactura),
constraint fk_DETALLES_FACTURAS_ARTICULOS foreign key (codArticulo)
references ARTICULOS (codArticulo)
)


-------------------------------------INSERTS------------------------------------------



-----------FORMAS DE PAGO-------------
-- Efectivo
INSERT INTO FORMAS_PAGO (formaPago) VALUES ('Efectivo');

-- Debito
INSERT INTO FORMAS_PAGO (formaPago) VALUES ('Debito');


-----------ARTICULOS-------------
-- Fernet Branca 750ml
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Fernet Branca 750ml', 3000, 1);
-- Coca Cola 2.25lts
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Coca Cola 2.25lts', 780, 1);
-- Coca Cola 1.5lts
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Coca Cola 1.5lts', 570, 1);
-- Chicle Topline7
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Chicle Topline7', 200, 1);
-- Arroz Gallo Oro
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Arroz Gallo Oro', 320, 1);
-- Leche LaSerenisima
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Leche LaSerenisima', 370, 1);
-- Alfajor Oreo Triple
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Alfajor Oreo Triple', 420, 1);
-- Alfajor Milka Triple Mouse
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Alfajor Milka Triple Mouse', 270, 1);
-- Encendedor Bic
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Encendedor Bic', 400, 1);
-- Marlboro 20u
INSERT INTO ARTICULOS (nombre, precioUnitario, activo) VALUES ('Marlboro 20u', 1000, 1);


-----------CLIENTES-------------
-- Cliente 1
INSERT INTO CLIENTES (apellido, nombre, documento) VALUES ('CONSUMIDOR FINAL', '', 00000000);
-- Cliente 2
INSERT INTO CLIENTES (apellido, nombre, documento) VALUES ('Apellido2', 'Nombre2', 23456789);
-- Cliente 3
INSERT INTO CLIENTES (apellido, nombre, documento) VALUES ('Apellido3', 'Nombre3', 34567890);

select * from CLIENTES