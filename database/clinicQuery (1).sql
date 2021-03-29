
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
nombre varchar(50),
apellido varchar(50),
telefono varchar(15),
correo varchar(50),
idpuesto int,
genero varchar(20),
contraseña varchar(20),
estado bit
<<<<<<< HEAD
constraint pk_idempleado primary key(id_e),
=======
constraint pk_idempleado primary key(id_empleado),
>>>>>>> feature-database
constraint fk_Empleado_Puesto foreign key(idpuesto) references Clinica.puesto(id_puesto) 
)


create table Clinica.pacientes(
id_paciente varchar(50),
nombre varchar(50),
apellido varchar(50),
telefono varchar(15),
edad int,
genero varchar(20),
constraint pk_idp primary key(id_paciente)
)



create table Clinica.tratamiento(
id_tratamiento int IDENTITY(1,1) not null,
nombre varchar(50),
precio int,
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
<<<<<<< HEAD
constraint fk_idt foreign key(id_t1) references Clinica.tratamiento(id_t)
=======
constraint fk_idt foreign key(id_tratamiento1) references Clinica.tratamiento(id_tratamiento)
>>>>>>> feature-database
on update no action
on delete no action
)


create table Clinica.historial(
id_historial int IDENTITY(1,1) not null,
id_paciente varchar(50),
fecha date,
id_t2 int,
constraint pk_idh primary key(id_historial),
constraint fk_idp foreign key(id_paciente) references Clinica.pacientes(id_paciente),
constraint fk_idt2 foreign key(id_t2) references Clinica.tratamiento(id_tratamiento)
)

create table Clinica.transaccion(
id_transaccion int IDENTITY(1,1) not null,
tipo_transacción varchar(15),
cantidad int,
fecha date,
dinero_disponible int,
<<<<<<< HEAD
constraint pk_idtrans primary key(id_trans)
=======
constraint pk_idtrans primary key(id_transaccion)
>>>>>>> feature-database
)

create table Clinica.citas(
id_empleado varchar(50),
id_paciente varchar(50),
fechaCita datetime,
id_tratamiento int,
<<<<<<< HEAD
constraint fk_Empleado_Citas foreign key(id_empleado) references Clinica.empleado(id_e),
constraint fk_Paciente_Citas foreign key(id_paciente) references Clinica.pacientes(id_p),
=======
constraint fk_Empleado_Citas foreign key(id_empleado) references Clinica.empleado(id_empleado),
constraint fk_Paciente_Citas foreign key(id_paciente) references Clinica.pacientes(id_paciente),
>>>>>>> feature-database
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
<<<<<<< HEAD
check ([id_e] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
=======
check ([id_empleado] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
>>>>>>> feature-database
GO

Alter table Clinica.pacientes WITH CHECK
add constraint CHK_Clinica_Pacientes$FormatoIdentidad
<<<<<<< HEAD
check ([id_p] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
=======
check ([id_paciente] LIKE '[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
>>>>>>> feature-database
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
@estado bit=1
as
insert into Clinica.empleado values(@id,@nombre,@apellido,@telefono,@correo,@puesto,@genero,@contraseña,@estado)
go


create proc	VerificarExistenciaEmpleado
@user varchar(50)
as 
<<<<<<< HEAD
select * from Clinica.empleado where id_e=@user
=======
select * from Clinica.empleado where id_empleado=@user
>>>>>>> feature-database
go


create proc MostrarEmpleados
as
select *from Clinica.empleado
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
@estado bit
as
<<<<<<< HEAD
update Clinica.empleado set nombre=@nombre,apellido=@apellido,telefono=@telefono,correo=@correo,idpuesto=@puesto,genero=@genero,contraseña=@contraseña, estado=@estado where id_e=@id
=======
update Clinica.empleado set nombre=@nombre,apellido=@apellido,telefono=@telefono,correo=@correo,idpuesto=@puesto,genero=@genero,contraseña=@contraseña, estado=@estado where id_empleado=@id
>>>>>>> feature-database
go


----TABLA PACIENTES

create proc IngresoPacientes
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@edad int,
@genero varchar(20)
as
insert into Clinica.pacientes values(@id,@nombre,@apellido,@telefono,@edad,@genero)
go


create proc Mostrarpacientes
as
select * from Clinica.pacientes 
go 


create proc MostrarPacienteEspecifico
@id varchar(50)
as
<<<<<<< HEAD
select *from Clinica.pacientes where id_p=@id
=======
select *from Clinica.pacientes where id_paciente=@id
>>>>>>> feature-database
go


create proc EditarPacientes
@id varchar(50),
@nombre varchar(50),
@apellido varchar(50),
@telefono varchar(15),
@edad int,
@genero varchar(20)
as
<<<<<<< HEAD
update Clinica.pacientes set nombre=@nombre,apellido=@apellido,telefono=@telefono,edad=@edad,genero=@genero where id_p=@id
=======
update Clinica.pacientes set nombre=@nombre,apellido=@apellido,telefono=@telefono,edad=@edad,genero=@genero where id_paciente=@id
>>>>>>> feature-database
go





----TABLA TRATAMIENTO
create proc IngresoTratamiento
@nombre varchar(50),
@precio int
as
insert into Clinica.tratamiento values(@nombre,@precio)
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
<<<<<<< HEAD
update Clinica.tratamiento set nombre=@nombre,precio=@precio where id_t=@id
=======
update Clinica.tratamiento set nombre=@nombre,precio=@precio where id_tratamiento=@id
>>>>>>> feature-database
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
<<<<<<< HEAD
select id_paciente,tratamiento.nombre,fecha from Clinica.historial inner join Clinica.tratamiento on historial.id_t2=tratamiento.id_t where @idpac=id_paciente
=======
select id_paciente,tratamiento.nombre,fecha from Clinica.historial inner join Clinica.tratamiento on historial.id_t2=tratamiento.id_tratamiento where @idpaciente=id_paciente
>>>>>>> feature-database
go
 

create proc EditarHistorial
@idpaciente varchar(50),
@fecha date,
@idtratamiento int,
@id int
as
<<<<<<< HEAD
update Clinica.historial set id_paciente=@idpaciente, id_t2=@idtratamiento, fecha=@fecha where id_h=@id
=======
update Clinica.historial set id_paciente=@idpaciente, id_t2=@idtratamiento, fecha=@fecha where id_historial=@id
>>>>>>> feature-database
go

create proc MostrarUso
@id int
as
<<<<<<< HEAD
select tratamiento.nombre,tratamiento.precio,tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_t1=tratamiento.id_t inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=id_material;
=======
select tratamiento.nombre,tratamiento.precio,tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_tratamiento1=tratamiento.id_tratamiento inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=id_material;
>>>>>>> feature-database
go


create proc materialnecesario
@id bigint
as
<<<<<<< HEAD
select inventario.nombre, tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_t1=tratamiento.id_t inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=tratamiento.id_t
go

create proc actCantidad
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

=======
select inventario.nombre, tratamiento_inventario.cantidad from Clinica.tratamiento_inventario inner join Clinica.tratamiento on tratamiento_inventario.id_tratamiento1=tratamiento.id_tratamiento inner join Clinica.inventario on tratamiento_inventario.id_material1=inventario.id_material where @id=tratamiento.id_tratamiento
go

create proc actCantidad
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

>>>>>>> feature-database
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
insert into Clinica.puesto values ('Doctor')
insert into Clinica.empleado values('0501200010557','Nallely','Reyes','87654329','nalle@gmail.com',1,'Femenino','123',1)


insert into empleado values('2020','Javier','Castro',98756845,'Javi@gmail.com','Dr','Masculino','123')
insert into empleado values('2007','Andres','Martinez',98934532,'andres@gmail.com','Dr','Masculino','123')
insert into empleado values ('admin','Admin','Admin',00000000,'admin@admin.com','Dr','No binario','admin')
/*Ingreso de tratamiento*/
insert into tratamiento values('Restauración',500)
insert into tratamiento values('Extracción',300)
insert into tratamiento values('Ortodoncia',1000)

select *from tratamiento

/*Ingreso de material*/

insert into inventario values('Recinas',100)
insert into inventario values('Gazas',80)
insert into inventario values('Anestecia',50)
insert into inventario values('Agujas',100)
insert into inventario values('Brackets',300)
insert into inventario values('Alambre',100)
insert into inventario values('Ules',500)
insert into inventario values('adhesivo',40)
insert into inventario values('Acido',50)

select * from inventario 
select * from tratamiento

/*Ingreso de relacion tratamiento y material
delete from tratamiento_inventario where id_material1 = 3
select * from tratamiento_inventario
insert into tratamiento_inventario values(1,1,2)
insert into tratamiento_inventario values(2,2,3)
insert into tratamiento_inventario values(4,3,1)
insert into tratamiento_inventario values(5,3,28)
insert into tratamiento_inventario values(6,3,2)
insert into tratamiento_inventario values(7,3,28)
insert into tratamiento_inventario values(8,3,4)
insert into tratamiento_inventario values(9,3,1)
insert into tratamiento_inventario values(3,3,1)
insert into tratamiento_inventario values(3,2,1)
insert into tratamiento_inventario values(3,1,1)
insert into tratamiento_inventario values(4,2,1)
insert into tratamiento_inventario values(4,1,1)
<<<<<<< HEAD

*/








=======
*/
>>>>>>> feature-database
