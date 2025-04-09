from django.shortcuts import render, redirect, get_object_or_404
from django.contrib.auth.forms import UserCreationForm, AuthenticationForm
from django.contrib.auth.models import User
from django.contrib.auth import login, logout, authenticate
from django.db  import IntegrityError
from django.http import HttpResponse
from Inventario.forms import StockForm
from Inventario.models import Stock_1


# Create your views here.

def Home (request):
    return render (request, 'Home.html')

    
def SingUp (request):

    if request.method == 'GET':
        return render(request, 'SingUp.html',{
        'form' : UserCreationForm})
    

    else:
        if request.POST['password1'] == request.POST['password2']:
            #register User
            try:
                user = User.objects.create_user(username=request.POST['username'],
                                     password=request.POST['password1'])
                user.save()
                login(request, user)
                return redirect('Stock')
                
            
            except IntegrityError:
                return render(request, 'SingUp.html',{
                'form' : UserCreationForm,
                "error": 'El Usario ya existe'
                })


        return render(request, 'SingUp.html',{
                'form' : UserCreationForm,
                "error": 'Las constrasenas no coinciden'
                })
    
def Stock(request):
    
    Stock = Stock_1.objects.filter(user = request.user)
    return render (request, 'Stock.html',{'stoc': Stock})

def Stock_detail(request, Stock_id):
    
    if request.method == 'GET':
        Stock = get_object_or_404(Stock_1, pk=Stock_id, user = request.user)
        form = StockForm(instance=Stock)
        return render(request, 'Stock_detail.html', {'stoc': Stock, 'form' : form})
    
    else:
        Stock = get_object_or_404(Stock_1, pk=Stock_id, user = request.user)
        form = StockForm(request.POST, instance= Stock)
        form.save()
        return redirect('Stock')


def Crear(request):
    
    if request.method == 'GET':
        return render(request, 'Crear.html',{
        'form' : StockForm
        })
    
    else:
        try:
            form = StockForm(request.POST)
            nuevo_stock = form.save(commit=False)
            nuevo_stock.user = request.user
            nuevo_stock.save()
            return redirect('Stock')
        
        except ValueError:
            return render(request, 'Crear.html',{
            'form' : StockForm,
            'error': 'Ingrese datos validos'
            })
    
    




def LogOut(request):
    logout(request)
    return redirect('Home')

def LogIn(request):
    
    if request.method == 'GET':
        return render(request, 'LogIn.html',{
            'form': AuthenticationForm
        })
    
    
    else:
        
        user = authenticate(request, username = request.POST['username'], 
                     password = request.POST['password']  )
        
        if user is None:
            return render(request, 'LogIn.html',{
            'form': AuthenticationForm,
            'error': 'La contrasela es incorrecta'
            })
        else:
            login(request, user)
            return redirect('Stock')
            


        
    
    
