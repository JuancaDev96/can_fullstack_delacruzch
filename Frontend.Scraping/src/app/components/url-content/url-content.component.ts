import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ArchivoService } from '../../Services/Archivo.service';
import { Contenido } from '../../Models/Contenido';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-url-content',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './url-content.component.html',
  styleUrl: './url-content.component.css'
})
export class UrlContentComponent {
  contenidos: Contenido[] = []; // Esto espera una lista de contenidos
  cargando = true;
  error: string | null = null;
  idUrl!: number;

  constructor(
    private contenidoService: ArchivoService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.idUrl = Number(this.route.snapshot.paramMap.get('idUrl'));
    if (!this.idUrl) {
      this.error = 'ID de URL no válido.';
      return;
    }
    this.cargarContenido();
  }

  cargarContenido(): void {
    this.contenidoService.obtenerContenidoPorUrl(this.idUrl).subscribe({
      next: (data) => {
        // Aseguramos que data es un arreglo, si no, lo convertimos en arreglo
        this.contenidos = Array.isArray(data) ? data : [data];
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
