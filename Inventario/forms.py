from django.forms import ModelForm
from Inventario.models import Stock_1



class StockForm(ModelForm):
    class Meta:
        model= Stock_1
        fields = ['Titulo', 'Descripcion', 'Cantidad', 'Comprar']