import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../Services/Auth.service';
import { Login } from '../../Models/Login';
import { FormsModule } from '@angular/forms';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterLink, NgIf,FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  
  usuario: Login = {usuario: '', clave: ''}; 

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
      const usuario: Login = this.usuario;
      this.authService.iniciarSesion(usuario).subscribe({
        next: () => {
          this.router.navigate(['/app']);  // Ajusta la ruta de redirección según tu necesidad
        },
        error: (error) => alert(`Error en el inicio de sesión: ${error}`)
      });
  }
  
}
