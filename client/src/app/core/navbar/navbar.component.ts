import { Component, OnInit } from '@angular/core';
import {BasketService} from "../../basket/basket.service";
import {Observable} from "rxjs";
import {IBasket} from "../../shared/models/basket";
import {IUser} from "../../shared/models/user";
import {AccountService} from "../../account/account.service";

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {
  basket$: Observable<IBasket>;
  currentUser$: Observable<IUser>;

  constructor(private basketService: BasketService, private accountService: AccountService) { }

  ngOnInit(): void {
    this.basket$ =this.basketService.basket$;
    this.currentUser$ = this.accountService.currentUser$;
  }

  logout(){
    this.accountService.logout();
  }
}
