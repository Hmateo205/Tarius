from django.db import models
from django.contrib.auth.models import User

# Create your models here.
class Inventario(models.Model):
    titulo = models.CharField
    Descripcion =models.TextField
    Cantidad = models.IntegerField
    Fecha_Creacion =models.DateField (auto_now_add=True)
    Comprar =   models.BooleanField(default=False)
    user = models.ForeignKey(User, on_delete=models.CASCADE)