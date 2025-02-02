import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ArchivoService } from '../../Services/Archivo.service';
import { Direccion } from '../../Models/Direccion';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-file-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './file-detail.component.html',
  styleUrl: './file-detail.component.css'
})
export class FileDetailComponent {
  direcciones: Direccion[] = [];
  cargando = true;
  error: string | null = null;
  archivoId!: number;

  constructor(
    private archivoService: ArchivoService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.archivoId = Number(this.route.snapshot.paramMap.get('archivoId'));
    if (!this.archivoId) {
      this.error = 'ID de archivo no válido.';
      return;
    }
    this.cargarDirecciones();
  }

  cargarDirecciones(): void {
    this.archivoService.obtenerDireccionesPorArchivo(this.archivoId).subscribe({
      next: (data) => {
        this.direcciones = data;
        this.cargando = false;
      },
      error: (err) => {
        this.error = err.message;
        this.cargando = false;
      }
    });
  }

  regresar(): void {
    this.router.navigate(['/app/archivos']);
  }
}
