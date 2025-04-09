from django.forms import ModelForm
from Inventario.models import Stock



class StokcForm(ModelForm):
    class Meta:
        model= Stock
        fields = ['Titulo', 'Descripcion', 'Comprar']