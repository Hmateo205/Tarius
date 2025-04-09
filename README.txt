Sistema de Inventario en Django

Este proyecto es una aplicación web construida con **Django** que permite gestionar productos en stock, visualizar detalles y editar la información mediante formularios. Incluye funcionalidades de autenticación para que los usuarios puedan iniciar sesión y registrar nuevos productos asociados a su cuenta.

---

##  Características

-  Registro de usuarios (signup).
-  Inicio de sesión y cierre de sesión.
-  Visualización de productos en stock.
-  Visualización de detalles por producto.
-  Edición de productos mediante formularios.
-  Relación de cada producto con su creador (usuario).
-  Interfaz web sencilla y funcional.



In CMD
pip install django
python manage.py makemigrations
python manage.py migrate
python manage.py runserver