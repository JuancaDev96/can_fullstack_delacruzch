import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './Services/Auth.guard';
import { FileListComponent } from './components/file-list/file-list.component';
import { LayoutComponent } from './general/layout/layout.component';
import { RegistrarComponent } from './components/registrar/registrar.component';
import { FileUploadComponent } from './components/upload-file/file-upload.component';
import { FileDetailComponent } from './components/file-detail/file-detail.component';
import { UrlContentComponent } from './components/url-content/url-content.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'registrar', component: RegistrarComponent },
  { 
    path: 'app', 
    component: LayoutComponent,
    canActivate: [AuthGuard], 
    children: [
      { path: 'archivos', component: FileListComponent },
      { path: 'subir-archivo', component: FileUploadComponent},
      { path: 'direcciones/:archivoId', component: FileDetailComponent},
      { path: 'contenido/:idUrl', component: UrlContentComponent},
      { path: '', redirectTo: 'archivos', pathMatch: 'full' }
    ]
  }
];