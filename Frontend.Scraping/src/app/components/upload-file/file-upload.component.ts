import { Component } from '@angular/core';
import { ArchivoService } from '../../Services/Archivo.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-file-upload',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent {
  archivoSeleccionado: File | null = null;
  mensaje: string | null = null;
  error: string | null = null;
  cargando = false;

  constructor(private archivoService: ArchivoService, private router: Router) {}

  seleccionarArchivo(event: any): void {
    if (event.target.files.length > 0) {
      this.archivoSeleccionado = event.target.files[0];
    }
  }

  subirArchivo(): void {
    if (!this.archivoSeleccionado) {
      this.error = 'Por favor, seleccione un archivo.';
      return;
    }

    this.cargando = true;
    this.archivoService.subirArchivo(this.archivoSeleccionado).subscribe({
      next: () => {
        this.mensaje = 'Archivo subido correctamente';
        this.error = null;
        setTimeout(() => this.router.navigate(['/app/archivos']), 1500);
      },
      error: (err) => {
        this.error = err.message;
        this.mensaje = null;
      },
      complete: () => this.cargando = false
    });
  }
}
