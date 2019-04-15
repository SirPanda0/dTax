import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import * as $ from 'jquery';
import { NgForm} from '@angular/forms';
declare var ymaps: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  DistanTest;
  constructor(private http: HttpService) { }

  ngOnInit() {
    ymaps.ready(this.init);
  }
  init() {
    let Distan;

    const myMap = new ymaps.Map('map', { // Выводим в слой с id="map"
    center: [57.767961, 40.926858], // центрируем карту на Кострому
    zoom: 11, // Ставим зум кратный 11
    controls: [] // Никакие кнопки управления пока не добавляем
    });
    // tslint:disable-next-line:one-variable-per-declaration
    const routePanelControl = new ymaps.control.RoutePanel({
    options: {
        showHeader: true, // Показывать заголовок навигационной панели
        title: 'Проложить маршрут', // Заголовок панели
        allowSwitch: true, // Отображать кнопку для смены местами Откуда/Куда
        maxWidth: '300px', // Ширина панели (макс. 400px)
        float: 'left' // Расположение в левой части карты
    }
});
    const zoomControl = new ymaps.control.ZoomControl({
    options: {
        float: 'right', // Расположение в правой части карты
        position: {     //Координаты 
            top: 245,   //расположения
            right: 10   //кнопок зума
        }
    }
});
    myMap.controls.add(routePanelControl).add(zoomControl);

    routePanelControl.routePanel.options.set({
      types: {auto: true},
   });

    routePanelControl.routePanel.geolocate('from');

    // Получим ссылку на маршрут.
    // tslint:disable-next-line:only-arrow-functions
    routePanelControl.routePanel.getRouteAsync().then(function(route) {
      // Зададим максимально допустимое число маршрутов, возвращаемых мультимаршрутизатором.
      route.model.setParams({results: 1}, true);
      //Искать оптимальный маршрут с учетом пробок
      route.model.setParams({avoidTrafficJams: true}, true);
      // Повесим обработчик на событие построения маршрута.
      // tslint:disable-next-line:only-arrow-functions
      route.model.events.add('requestsuccess', function() {
          const activeRoute = route.getActiveRoute();
          if (activeRoute) {
            length = route.getActiveRoute().properties.get("distance");
            Distan = length;
            $('#Dist').val(Distan.value);
            console.log(Distan);
            activeRoute.balloon.open();
          }
      });
  });
}

  test(form: NgForm) {
    const Distan = $('#Dist').val();
    this.http.get('CabRide/GetRidePrice?distance=' + Distan).subscribe(data => {
      this.DistanTest = data;
    })
  }
}
