from django.db import models
from django.contrib.auth.models import User

# Create your models here.
class Stock(models.Model):
    Titulo = models.CharField(max_length=100)
    Descripcion =models.TextField(blank=True)
    Cantidad = models.IntegerField(max_length=100)
    Fecha_Creacion =models.DateField (auto_now_add=True)
    Comprar =   models.BooleanField(default=False)
    user = models.ForeignKey(User, on_delete=models.CASCADE)
    
    def __str__ (self):
        return self.Titulo + '/ Creado por:' + self.user.username