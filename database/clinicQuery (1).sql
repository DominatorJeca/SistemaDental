
create database clinicaDental
use clinicaDental

create schema Clinica
go

--Creación de tablas
create table Clinica.puesto(
id_puesto int identity(1,1) not null,
nombrePuesto varchar(50)
constraint pk_id primary key(id_puesto)
)

create table Clinica.empleado(
id_empleado varchar(50),
nombre varchar(50) not null,
apellido varchar(50) not null,
telefono varchar(15) not null,
correo varchar(50),
idpuesto int not null,
genero varchar(20) not null,
contraseña varchar(20)not null,
estado bit default 1,
administrador bit default 0
constraint pk_idempleado primary key(id_empleado),
constraint fk_Empleado_Puesto foreign key(idpuesto) references Clinica.puesto(id_puesto) 
)


create table Clinica.pacientes(
id_paciente varchar(50),
nombre varchar(50),
apellido varchar(50),
telefono varchar(15),
edad int,
genero varchar(20),
estado bit default 1,
constraint pk_idp primary key(id_paciente)
)



create table Clinica.tratamiento(
id_tratamiento int IDENTITY(1,1) not null,
nombre varchar(50) not null,
precio int not null,
estado bit default 1,
constraint pk_idt primary key(id_tratamiento)
)


create table Clinica.inventario(
id_material int IDENTITY(1,1) not null,
nombre varchar(50),
cantidad int,
constraint pk_idm primary key(id_material)
)

create table Clinica.tratamiento_inventario(
id_material1 int,
id_tratamiento1 int,
cantidad int,
constraint fk_idm foreign key(id_material1) references Clinica.inventario(id_material),
constraint fk_idt foreign key(id_tratamiento1) references Clinica.tratamiento(id_tratamiento)
on update no action
on delete no action
)


create table Clinica.historial(
id_historial int IDENTITY(1,1) not null,
id_paciente varchar(50),
fecha date,
id_tratamiento2 int,
constraint pk_idh primary key(id_historial),
constraint fk_idp foreign key(id_paciente) references Clinica.pacientes(id_paciente),
constraint fk_idt2 foreign key(id_tratamiento2) references Clinica.tratamiento(id_tratamiento)
)

create table Clinica.transaccion(
id_transaccion int IDENTITY(1,1) not null,
tipo_transacción varchar(15),
cantidad int,
fecha date,
dinero_disponible int,
constraint pk_idtrans primary key(id_transaccion)
)

create table Clinica.citas(
id_empleado varchar(50),
id_paciente varchar(50),
fechaCita datetime,
id_tratamiento int,
constraint fk_Empleado_Citas foreign key(id_empleado) references Clinica.empleado(id_empleado),
constraint fk_Paciente_Citas foreign key(id_paciente) references Clinica.pacientes(id_paciente),
)

--Constraints

ALTER TABLE Clinica.historial with check
add constraint CHK_Clinica_Historial$VerificarFechaTratamiento
Check (fecha>= GETDATE())
GO

ALTER TABLE Clinica.citas with check
add constraint CHK_Clinica_Citas$VerificarFechaCita
check (fechaCita>GETDATE())
GO

Alter table Clinica.inventario with check
add constraint CHK_Clinica_Inventario$VerificaQueLaCantidadNoSeaMenorACero
check (cantidad>=0)


Alter table Clinica.empleado with check
add constraint CHK_Clinica_Empleado$FormatoTelefonoEmpleado
check (LEN(telefono)>=8)

Alter table Clinica.pacientes with check
add constraint CHK_Clinica_Paciente$FormatoTelefonoEmpleado
check (LEN(telefono)>=8)

Alter table Clinica.empleado with check
add constraint CHK_Clinica_Empleado$FormatoTelefono
check (telefono LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
GO

Alter table Clinica.pacientes with check
add constraint CHK_Clinica_Paciente$FormatoTelefono
check (telefono LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
GO

Alter table Clinica.empleado WITH CHECK
add constraint CHK_Clinica_Empleados$FormatoIdentidad
check ([id_empleado] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
GO

Alter table Clinica.pacientes WITH CHECK
add constraint CHK_Clinica_Pacientes$FormatoIdentidad
check ([id_paciente] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
GO

alter table Clinica.empleado with check
add constraint CHK_Clinica_Empleado$Genero
check (genero in('Femenino','Masculino'))
go

alter table Clinica.pacientes with check
add constraint CHK_Clinica_Paciente$Genero
check (genero in('Femenino','Masculino'))
go

alter table Clinica.empleado with check
add constraint CHK_Clinica_Empleado$ContraseñaSegura
check (LEN(contraseña)>6)
go

alter table Clinica.pacientes with check
add constraint CHK_Clinica_Paciente$EdadCorrecta
check (edad>0)
go

alter table Clinica.tratamiento with check
add constraint CHK_Clinica_Tratamiento$PrecioCorrecto
check (precio>0)
go

alter table Clinica.tratamiento 
add constraint UNQ_Clinica_Tratamiento$NombreTratamiento
unique nonclustered (nombre)
go

alter table Clinica.tratamiento_inventario with check
add constraint CHK_Clinica_TratamientoInventario$CantidadDeMaterial
check (cantidad>0)
go

alter table Clinica.transaccion with check
add constraint CHK_Clinica_Transaccion$CantidadCorrecta
check (cantidad>0)
go

alter table Clinica.transaccion with check
add constraint CHK_Clinica_Transaccion$DineroDisponible
check (dinero_disponible>=0)
go

alter table Clinica.transaccion with check
add constraint CHK_Clinica_Transaccion$FechaCorrecta
check (fecha>=GETDATE())
go

alter table Clinica.puesto with check
add constraint CHK_Clinica_Puesto$NombrePuestoCorrecto
check (nombrePuesto Like 'Doctor' or nombrePuesto Like 'Asistente' or nombrePuesto Like 'Secretaria' or nombrePuesto Like 'Secretario')
go

-----PROCEDIMIENTOS
-----TABLA EMPLEADOS
create proc IngresoEmpleados
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@correo varchar(50),
@puesto int,
@genero varchar(20),
@contraseña varchar(20),
@estado bit=1,
@administrador bit=0
as
insert into Clinica.empleado values(@id,@nombre,@apellido,@telefono,@correo,@puesto,@genero,@contraseña,@estado,@administrador)
go


create proc	VerificarExistenciaEmpleado
@user varchar(50)
as 
select * from Clinica.empleado where id_empleado=@user
go


create proc MostrarEmpleados
as
select *from Clinica.empleado
go

create proc MostrarEmpleadosActivos
as
select *from Clinica.empleado
where estado=1
go

create proc EditarEmpleados
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@correo varchar(50),
@puesto int,
@genero varchar(20),
@contraseña varchar(20),
@estado bit=1,
@administrador bit=0
as
update Clinica.empleado set nombre=@nombre,apellido=@apellido,telefono=@telefono,correo=@correo,idpuesto=@puesto,genero=@genero,contraseña=@contraseña where id_empleado=@id
go

create proc EliminarUsuario
@id varchar(50)
as
update Clinica.empleado set estado=0 where id_empleado=@id
go

create proc PrivilegioAdministrador
@id varchar(50)
as
update Clinica.empleado set [administrador]=1
go

----TABLA PACIENTES

create proc IngresoPacientes
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@edad int,
@genero varchar(20),
@estado bit=1
as
insert into Clinica.pacientes values(@id,@nombre,@apellido,@telefono,@edad,@genero,@estado)
go


create proc Mostrarpacientes
as
select * from Clinica.pacientes 
go 


create proc MostrarPacienteEspecifico
@id varchar(50)
as
select *from Clinica.pacientes where id_paciente=@id
go


create proc EditarPacientes
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@edad int,
@genero varchar(20)
as
update Clinica.pacientes set nombre=@nombre,apellido=@apellido,telefono=@telefono,edad=@edad,genero=@genero where id_paciente=@id
go

create proc EliminarPaciente
@id varchar(50)
as
update Clinica.pacientes set estado=0
go



----TABLA TRATAMIENTO
create proc IngresoTratamiento
@nombre varchar(50),
@precio int,
@estado bit=1
as
insert into Clinica.tratamiento values(@nombre,@precio,@estado)
go

create proc MostrarTratamiento
as
select *from Clinica.tratamiento
go

create proc mostrartratamientoSeleccionado
@name varchar(50)
as
select *from Clinica.tratamiento where @name=nombre
go

create proc EditarTratamiento
@nombre varchar(50),
@precio int,
@id int
as
update Clinica.tratamiento set nombre=@nombre,precio=@precio where id_tratamiento=@id
go

create proc EliminarTratamiento
@nombre varchar(50)
as
update Clinica.tratamiento set estado=0 where nombre=@nombre
go

-----TABLA INVENTARIO
create proc IngresoMaterial
@nombre varchar(50),
@cantidad int
as
insert into Clinica.inventario values(@nombre,@cantidad)
go

create proc MostrarInventario
as
select *from Clinica.inventario
go

create proc MostrarPuesto
as
select *from Clinica.puesto
go

create proc MostrarUnPuesto
@id int
as
select nombrePuesto from Clinica.puesto
where id_puesto=@id
go

create proc EditarInventario
@nombre varchar(50),
@cantidad int,
@id int
as
update Clinica.inventario set nombre=@nombre,cantidad=@cantidad where id_material=@id
go

---------TABLA TRANSACCION
create proc IngresoTransaccion
@tipo varchar(15),
@cantidad int,
@fecha date,
@dinerodisponible int
as
insert into Clinica.transaccion values(@tipo,@cantidad,@fecha,@dinerodisponible)
go



create proc MostrarTransaccion
as
select *from Clinica.transaccion
go

------TABLA HISTORIAL
create proc IngresoHistorial
@idpaciente varchar(50),
@fecha date,
@idtratamiento int
as
insert into Clinica.historial values(@idpaciente,@fecha,@idtratamiento)
go

create proc MostrarHistorial
@idpaciente varchar(50)
as
select id_paciente,tratamiento.nombre,fecha from Clinica.historial inner join Clinica.tratamiento on historial.id_tratamiento2=tratamiento.id_tratamiento where @idpaciente=id_paciente
go
 

create proc EditarHistorial
@idpaciente varchar(50),
@fecha date,
@idtratamiento int,
@id int
as
update Clinica.historial set id_paciente=@idpaciente, id_tratamiento2=@idtratamiento, fecha=@fecha where id_historial=@id
go

create proc MostrarUso
@id int
as
select tratamiento.nombre,tratamiento.precio,tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_tratamiento1=tratamiento.id_tratamiento inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=id_material;
go


create proc materialnecesario
@id bigint
as
select inventario.nombre, tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_tratamiento1=tratamiento.id_tratamiento inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=tratamiento.id_tratamiento
go

create proc actualizarCantidad
@cantidad int,
@material varchar(50)
as
update Clinica.inventario set cantidad=cantidad-@cantidad where nombre=@material
go


/*Tabla Citas*/
create proc IngresoCitas
@idempleado varchar(50),
@idpaciente varchar(50),
@fecha datetime,
@idtratamiento int
as
insert into Clinica.citas values (@idempleado,@idpaciente,@fecha,@idtratamiento)
go


create proc EditarCitas
@idempleado varchar(50),
@idpaciente varchar(50),
@fecha datetime,
@idtratamiento int
as
update Clinica.citas set id_empleado=@idempleado,id_tratamiento=@idtratamiento,fechaCita=@fecha where id_paciente=@idpaciente
go

create proc MostrarCitas
as
select *from Clinica.citas
go

create proc EliminarCitas
@idpaciente varchar(50)
as
delete Clinica.citas where id_paciente=@idpaciente
go

--inserts--


/*Ingreso de empleados*/
insert into Clinica.puesto values ('Secretaria')
insert into Clinica.empleado values('0501200010558','Andrea','Murillo','33986418','andrea@gmail.com',3,'Femenino','andrealomaximo',1)



/*Ingreso de tratamiento*/
insert into Clinica.tratamiento values('Restauración',500)
insert into Clinica.tratamiento values('Extracción',300)
insert into Clinica.tratamiento values('Ortodoncia',1000)

/*Ingreso de material*/
insert into Clinica.inventario values('Recinas',100),('Gazas',80),('Anestecia',50)
,('Agujas',100)
,('Brackets',300)
,('Alambre',100)
,('Ules',500)
,('adhesivo',40)
,('Acido',50)





--Ingreso de relacion tratamiento y material

insert into Clinica.tratamiento_inventario values(1,1,2)
,(2,2,3)
,(4,3,1)
,(5,3,28)
,(6,3,2)
,(7,3,28)
,(8,3,4)
,(9,3,1)
,(3,3,1)
,(3,2,1)
,(3,1,1)
,(4,2,1)
,(4,1,1)

