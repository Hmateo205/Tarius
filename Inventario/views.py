from django.shortcuts import render
from django.contrib.auth.forms import UserCreationForm
from django.contrib.auth.models import User
from django.http import HttpResponse

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
            
                return HttpResponse('Usuario Creado')
            
            except:
                return render(request, 'SingUp.html',{
                'form' : UserCreationForm,
                "error": 'El Suario ya existe'
                })


        return render(request, 'SingUp.html',{
                'form' : UserCreationForm,
                "error": 'Las constrasenas no coinciden'
                })