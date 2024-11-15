import { Routes } from '@angular/router';
import { LandingComponent } from '../components/landing/landing.component';
import { TherapistsCatalogComponent } from './therapists/catalog/therapists-catalog.component';

export const routes: Routes = [
    {
        path: '',
        component: LandingComponent,
        title: 'Home Page',
    },
    {
        path: 'therapists',
        component: TherapistsCatalogComponent,
        title: 'Therapists Catalog',
    },
    {
        path: '**',
        redirectTo: '',
    },
]
