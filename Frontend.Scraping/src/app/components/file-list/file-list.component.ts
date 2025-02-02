import { Component, OnInit } from '@angular/core';
import { ArchivoService } from '../../Services/Archivo.service';
import { Archivo } from '../../Models/Archivo';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-file-list',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './file-list.component.html',
  styleUrls: ['./file-list.component.css']
})
export class FileListComponent implements OnInit {
  archivos: Archivo[] = [];
  cargando = true;
  error: string | null = null;

  constructor(private archivoService: ArchivoService) {}

  ngOnInit(): void {
    this.cargarArchivos();
  }

  cargarArchivos(): void {
    this.archivoService.obtenerArchivos().subscribe({
      next: (data) => {
        this.archivos = data;
        this.cargando = false;
      },
      error: (err) => {
        this.error = err.message;
        this.cargando = false;
      }
    });
  }
}
