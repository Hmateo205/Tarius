"""
URL configuration for Proyuecto_HJ project.

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/5.2/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path
from Inventario import views

urlpatterns = [
    path('admin/', admin.site.urls),
    path ('', views.Home, name='Home'),
    path('SingUp/',views.SingUp, name = 'SingUp'),
    path('Stock/',views.Stock, name= 'Stock'),
    path('LogOut/',views.LogOut, name= 'LogOut'),
    path('LogIn/',views.LogIn, name= 'LogIn'),
    path('Stock/Crear/',views.Crear, name= 'Crear'),
    path('Stock/<int:Stock_id>/',views.Stock_detail, name= 'Stock_detail'),
    path('Stock/<int:Stock_id>/Eliminar',views.Stock_delete, name= 'Stock_delete'),






]
