import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../Services/Auth.service';
import { CommonModule } from '@angular/common';
import { Usuario } from '../../Models/Usuario';

@Component({
  selector: 'app-registrar',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule], 
  templateUrl: './registrar.component.html',
  styleUrls: ['./registrar.component.css'] 
})
export class RegistrarComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      nombre: ['', Validators.required],
      correo: ['', [Validators.required, Validators.email]],
      nombreUsuario: ['', Validators.required],
      clave: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const usuario: Usuario = this.registerForm.value;
      this.authService.registrarUsuario(usuario).subscribe({
        next: () => {
          alert('Registro exitoso');
          this.router.navigate(['/login']);
        },
        error: (error) => {
          alert(`Error en el registro: ${error}`);
        }
      });
    } else {
      alert('Por favor, complete todos los campos correctamente');
    }
  }
  
}
