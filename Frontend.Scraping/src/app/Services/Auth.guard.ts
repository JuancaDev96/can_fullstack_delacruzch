import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router, @Inject(PLATFORM_ID) private platformId: any) {}

  canActivate(): boolean {
    // Verificar si está en el navegador antes de acceder a sessionStorage
    if (isPlatformBrowser(this.platformId)) {
      const token = sessionStorage.getItem('token');
      if (token) {
        return true;
      }
    }
    
    this.router.navigate(['/login']);
    return false;
  }
}
