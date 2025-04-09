from django.shortcuts import render, redirect
from django.contrib.auth.forms import UserCreationForm, AuthenticationForm
from django.contrib.auth.models import User
from django.contrib.auth import login, logout, authenticate
from django.db  import IntegrityError
from django.http import HttpResponse
from Inventario.forms import StokcForm

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
    return render (request, 'Stock.html')

def Crear(request):
    
    if request.method == 'GET':
        return render(request, 'Crear.html',{
        'form' : StokcForm
        })
    
    else:
        print(request.POST)
        return render(request, 'Crear.html',{
        'form' : StokcForm
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
            


        
    
    
