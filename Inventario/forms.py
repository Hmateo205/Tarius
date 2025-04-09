from django.forms import ModelForm
from Inventario.models import Stock



class StockForm(ModelForm):
    class Meta:
        model= Stock
        fields = ['Titulo', 'Descripcion', 'Cantidad', 'Comprar']