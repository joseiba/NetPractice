create or replace package wilson1.pgk_municipalidad_empleados is

  -- Author  : JOSEA
  -- Created : 02/05/2020 16:31:17
  -- Purpose : 
  
 procedure sp_get_all_ciudades(po_cursor out sys_refcursor);
  procedure sp_insert_ciudad(pi_nombre in wilson1.ciudad.nombre%type,
                             pi_ubicacion_geografica in wilson1.ciudad.ubicacion_geografica%type);
 procedure sp_delete_ciudad(pi_id_ciudad in wilson1.ciudad.id_ciudad%type);
                          
 procedure sp_update_ciudad(pi_id_ciudad in wilson1.ciudad.id_ciudad%type,
                             pi_nombre in wilson1.ciudad.nombre%type,
                             pi_ubicacion_geografica in wilson1.ciudad.ubicacion_geografica%type);

 procedure sp_get_all_municipalidades(po_cursor out sys_refcursor);


 procedure sp_insert_municipalidades(pi_nombre in wilson1.municipalidades.nombre_municipalidad%type,
                             pi_direccion in wilson1.municipalidades.direccion%type,
                             pi_telefono in wilson1.municipalidades.telefono%type,
                             pi_correo_electronico in wilson1.municipalidades.correo_electronico%type,
                             pi_id_ciudad in wilson1.municipalidades.id_ciudad%type);
                             
 procedure sp_update_municipalidad(pi_id_municipalidad in wilson1.municipalidades.id_municipalidad%type,
                             pi_nombre in wilson1.municipalidades.nombre_municipalidad%type,
                             pi_direccion in wilson1.municipalidades.direccion%type,
                             pi_telefono in wilson1.municipalidades.telefono%type,
                             pi_correo_electronico in wilson1.municipalidades.correo_electronico%type,
                             pi_id_ciudad in wilson1.municipalidades.id_ciudad%type);
  procedure sp_delete_municipalidad(pi_id_municipalidad in wilson1.municipalidades.id_municipalidad%type);
  
  -- This part is for cagos
 -- get all cargos
 procedure sp_get_all_cargos(po_cursor out sys_refcursor);
 -- insert cargo
 procedure sp_insert_cargo(pi_nombre in wilson1.cargo.nombre_cargo%type,
                             pi_descripcion in wilson1.cargo.descripcion%type);
 -- insert delete
 procedure sp_delete_cargo(pi_id_cargo in wilson1.cargo.id_cargo%type);
 -- update cargo
  procedure sp_update_cargo(pi_id_cargo in wilson1.cargo.id_cargo%type,
                             pi_nombre_cargo in wilson1.cargo.nombre_cargo%type,
                             pi_descripcion in wilson1.cargo.descripcion%type);
 -- This part is for empleados
 -- get all empleados
  procedure sp_get_all_empleados(po_cursor out sys_refcursor);
  -- insert empleado
  procedure sp_insert_empleados(pi_cedula in wilson1.empleados.cedula%type,
                             pi_nombre in wilson1.empleados.nombre%type,
                             pi_apellido in wilson1.empleados.apellido%type,
                             pi_direccion in wilson1.empleados.direccion%type,
                             pi_telefono in wilson1.empleados.telefono%type,
                             pi_id_cargo in wilson1.empleados.id_cargo%type,
                             pi_id_municipalidad in wilson1.empleados.id_municipalidad%type);
 -- delete empleado
  procedure sp_delete_empleados(pi_cedula in wilson1.empleados.cedula%type);
 -- update empleado
  procedure sp_update_empleados(pi_cedula in wilson1.empleados.cedula%type,
                             pi_nombre in wilson1.empleados.nombre%type,
                             pi_apellido in wilson1.empleados.apellido%type,
                             pi_direccion in wilson1.empleados.direccion%type,
                             pi_telefono in wilson1.empleados.telefono%type,
                             pi_id_cargo in wilson1.empleados.id_cargo%type,
                             pi_id_municipalidad in wilson1.empleados.id_municipalidad%type);
                             
function f_valida_registro(pi_nombre in wilson1.ciudad.nombre%type) return number;
 procedure sp_mensaje (pi_nombre in wilson1.ciudad.nombre%type,
                         po_mensaje out varchar2);
end pgk_municipalidad_empleados;
/
create or replace package body wilson1.pgk_municipalidad_empleados is


  -- Function and procedure implementations
 -- procedure get all ciudades
 procedure sp_get_all_ciudades(po_cursor out sys_refcursor)
 is
 begin
   open po_cursor for
        select * from wilson1.ciudad c
        order by c.id_ciudad;
  end sp_get_all_ciudades;
  -- end get all
  
  procedure sp_insert_ciudad(pi_nombre in wilson1.ciudad.nombre%type,
                             pi_ubicacion_geografica in wilson1.ciudad.ubicacion_geografica%type)
  is
  pi_id_ciudad wilson1.ciudad.id_ciudad%type;
  begin
    select nvl(max(c.id_ciudad),0) 
    into pi_id_ciudad 
    from wilson1.ciudad c;
    -- if
    if(f_valida_registro(pi_nombre)=0) then
      insert into wilson1.ciudad(id_ciudad,nombre,ubicacion_geografica)
       values(pi_id_ciudad + 1, pi_nombre, pi_ubicacion_geografica);
    end if;
      
  end sp_insert_ciudad; 
  
  function f_valida_registro(pi_nombre in wilson1.ciudad.nombre%type) return number
    is 
    v_nombre number;
    begin
      select nvl(count(c.nombre),0)
      into v_nombre
      from wilson1.ciudad c
      where c.nombre=initcap(pi_nombre)
      or c.nombre=pi_nombre;
      
      return v_nombre;
      end f_valida_registro;

  procedure sp_mensaje (pi_nombre in wilson1.ciudad.nombre%type,
                         po_mensaje out varchar2)
   is
   begin
     if(f_valida_registro(pi_nombre)=0) then
        po_mensaje:= 'Se ingreso correctamento';
     else 
       po_mensaje:= 'Ya existe un registro con ese nombre';
       end if;
     end;
    
  
   -- procedure delete ciudad
 procedure sp_delete_ciudad(pi_id_ciudad in wilson1.ciudad.id_ciudad%type)
   is
   begin
     delete from wilson1.ciudad c
     where c.id_ciudad = pi_id_ciudad;
   end sp_delete_ciudad;
  -- end delete
  
  -- procedure update ciudad
  procedure sp_update_ciudad(pi_id_ciudad in wilson1.ciudad.id_ciudad%type,
                             pi_nombre in wilson1.ciudad.nombre%type,
                             pi_ubicacion_geografica in wilson1.ciudad.ubicacion_geografica%type)
  is
  begin
    
    update wilson1.ciudad c
    set c.nombre=pi_nombre, c.ubicacion_geografica=pi_ubicacion_geografica
    where c.id_ciudad=pi_id_ciudad;
    
  end sp_update_ciudad;   
  -- end update
  
    -- procedure get all municipalidades
 procedure sp_get_all_municipalidades(po_cursor out sys_refcursor)
 is
 begin
   open po_cursor for
      select m.id_municipalidad,
             m.nombre_municipalidad,
             m.direccion,
             m.telefono,
             m.correo_electronico,
             m.id_ciudad
      from wilson1.municipalidades m join wilson1.ciudad c
      on c.id_ciudad=m.id_municipalidad
      order by m.id_municipalidad;
      
  end sp_get_all_municipalidades;
  -- end get all
  
    
  -- Procedure ìnsert municipalidad
  procedure sp_insert_municipalidades(pi_nombre in wilson1.municipalidades.nombre_municipalidad%type,
                             pi_direccion in wilson1.municipalidades.direccion%type,
                             pi_telefono in wilson1.municipalidades.telefono%type,
                             pi_correo_electronico in wilson1.municipalidades.correo_electronico%type,
                             pi_id_ciudad in wilson1.municipalidades.id_ciudad%type)
  is
  pi_id_municipalidad wilson1.municipalidades.id_municipalidad%type;
  begin
        
    select nvl(max(m.id_municipalidad),0) 
    into pi_id_municipalidad 
    from wilson1.municipalidades m;
    -- if
    insert into wilson1.municipalidades(id_municipalidad,
                                        nombre_municipalidad,
                                        direccion,
                                        telefono,
                                        correo_electronico,
                                        id_ciudad)
    values(pi_id_municipalidad + 1, pi_nombre, pi_direccion, pi_telefono,
           pi_correo_electronico, pi_id_ciudad);
      
  end sp_insert_municipalidades;   
  --end insert       
  
   -- procedure delete municipalidad
 procedure sp_delete_municipalidad(pi_id_municipalidad in wilson1.municipalidades.id_municipalidad%type)
   is
   begin
     delete from wilson1.municipalidades m
     where m.id_municipalidad= pi_id_municipalidad;
     
   end sp_delete_municipalidad;
  -- end delete     
  
   -- procedure update municipalidad
  procedure sp_update_municipalidad(pi_id_municipalidad in wilson1.municipalidades.id_municipalidad%type,
                             pi_nombre in wilson1.municipalidades.nombre_municipalidad%type,
                             pi_direccion in wilson1.municipalidades.direccion%type,
                             pi_telefono in wilson1.municipalidades.telefono%type,
                             pi_correo_electronico in wilson1.municipalidades.correo_electronico%type,
                             pi_id_ciudad in wilson1.municipalidades.id_ciudad%type)
  is
  begin
    
    update wilson1.municipalidades m
    set m.nombre_municipalidad=pi_nombre,
        m.direccion=pi_direccion,
        m.telefono=pi_telefono,
        m.correo_electronico=pi_correo_electronico,
        m.id_ciudad=pi_id_ciudad
    where m.id_municipalidad=pi_id_municipalidad;
    
  end sp_update_municipalidad;   
  -- end update
  
   --This part is for Cargos
  -- procedure get all cargos
   procedure sp_get_all_cargos(po_cursor out sys_refcursor)
 is
 begin
   open po_cursor for
      select  *
      from wilson1.cargo ca 
      order by ca.id_cargo;
      
  end sp_get_all_cargos;
  -- end get all
  
  -- Procedure ìnsert cargo
  procedure sp_insert_cargo(pi_nombre in wilson1.cargo.nombre_cargo%type,
                             pi_descripcion in wilson1.cargo.descripcion%type)
  is
  pi_id_cargo wilson1.cargo.id_cargo%type;
  begin
    
    select nvl(max(ca.id_cargo),0) 
    into pi_id_cargo 
    from wilson1.cargo ca;
    -- if
    insert into wilson1.cargo(id_cargo,
                              nombre_cargo,
                              descripcion)
    values( pi_id_cargo + 1, pi_nombre, pi_descripcion);
      
  end sp_insert_cargo;   
  --end insert            
  
  -- procedure delete cargo
 procedure sp_delete_cargo(pi_id_cargo in wilson1.cargo.id_cargo%type)
   is
   begin
     delete from wilson1.cargo ca
     where ca.id_cargo=pi_id_cargo;
     
   end sp_delete_cargo;
  -- end delete
  
  -- procedure update cargo
  procedure sp_update_cargo(pi_id_cargo in wilson1.cargo.id_cargo%type,
                             pi_nombre_cargo in wilson1.cargo.nombre_cargo%type,
                             pi_descripcion in wilson1.cargo.descripcion%type)
  is
  begin
    
    update wilson1.cargo ca
    set ca.nombre_cargo=pi_nombre_cargo, ca.descripcion=pi_descripcion
    where ca.id_cargo=pi_id_cargo;
    
  end sp_update_cargo;   
  -- end update
  
   -- This part is for EMPLEADOS
  -- procedure get all Empleados
   procedure sp_get_all_empleados(po_cursor out sys_refcursor)
 is
 begin
   open po_cursor for
      select *
      from wilson1.empleados em 
      -- join wilson1.municipalidades m
         --  on em.id_municipalidad=m.id_municipalidad
      --join wilson1.cargo ca
           --on ca.id_cargo=em.id_cargo
      order by em.cedula;
      
  end sp_get_all_empleados;
  -- end get all
  
  -- Procedure ìnsert empleados
  procedure sp_insert_empleados(pi_cedula in wilson1.empleados.cedula%type,
                             pi_nombre in wilson1.empleados.nombre%type,
                             pi_apellido in wilson1.empleados.apellido%type,
                             pi_direccion in wilson1.empleados.direccion%type,
                             pi_telefono in wilson1.empleados.telefono%type,
                             pi_id_cargo in wilson1.empleados.id_cargo%type,
                             pi_id_municipalidad in wilson1.empleados.id_municipalidad%type)
  is
  pi_salario wilson1.empleados.salario%type;
  
  begin
    pi_salario:=0;
    -- if
    insert into wilson1.empleados(cedula,
                                  nombre,
                                  apellido,
                                  telefono,
                                  direccion,
                                  salario,
                                  id_municipalidad,
                                  id_cargo)
    values(pi_cedula, pi_nombre, pi_apellido,pi_telefono, pi_direccion,
           pi_salario,pi_id_municipalidad, pi_id_cargo);
      
  end sp_insert_empleados;   
  --end insert            
  
  -- procedure delete municipalidad
 procedure sp_delete_empleados(pi_cedula in wilson1.empleados.cedula%type)
   is
   begin
     delete from wilson1.empleados em
     where em.cedula= pi_cedula;
     
   end sp_delete_empleados;
  -- end delete
  
  -- procedure update empleados
  procedure sp_update_empleados(pi_cedula in wilson1.empleados.cedula%type,
                             pi_nombre in wilson1.empleados.nombre%type,
                             pi_apellido in wilson1.empleados.apellido%type,
                             pi_direccion in wilson1.empleados.direccion%type,
                             pi_telefono in wilson1.empleados.telefono%type,
                             pi_id_cargo in wilson1.empleados.id_cargo%type,
                             pi_id_municipalidad in wilson1.empleados.id_municipalidad%type)
  is
  begin
    
    update wilson1.empleados em
    set em.cedula=pi_cedula,
        em.nombre=pi_nombre,
        em.apellido=pi_apellido,
        em.direccion=pi_direccion,
        em.telefono=pi_telefono,
        em.id_cargo=pi_id_cargo,
        em.id_municipalidad=pi_id_municipalidad
    where em.cedula=pi_cedula;
    
  end sp_update_empleados;   
  -- end update
    
  
end pgk_municipalidad_empleados;
/
