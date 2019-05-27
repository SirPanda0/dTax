import { Component, OnInit } from '@angular/core';
import { HttpService } from '../Servise/http.service';
import * as $ from 'jquery';
import { NgForm} from '@angular/forms';
import { UserService } from '../Servise/user.service';
declare var ymaps: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  User;

  Distanse;
  Cars;

  TypeCar;
  TypePayment;

  constructor(private http: HttpService, private user: UserService) { }

  ngOnInit() {
    this.User = this.user.GetCurrentUser();
    console.log(this.User)
    ymaps.ready(this.init);
  }
  init() {
    let Distan;
    let Points;

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
            Points = route.getActiveRoute().properties.get("boundedBy");
            // console.log(route.getActiveRoute().model.multiRoute.properties._data.rawProperties.RouterMetaData.Waypoints);
            $('#StartP1').val(route.getActiveRoute().model.multiRoute.properties._data.rawProperties.RouterMetaData.Waypoints[0].address);
            $('#EndP1').val(route.getActiveRoute().model.multiRoute.properties._data.rawProperties.RouterMetaData.Waypoints[1].address);
            Distan = length;
            $('#Dist').val(Distan.value);
            activeRoute.balloon.open();
          }
      });
  });
}

  GetDistanse() {
    const Distan = $('#Dist').val();
    this.http.get('CabRide/GetRidePrice?distance=' + Distan).subscribe(data => {
      this.Cars = data;
      this.TypeCar = this.Cars.comfort;
      this.Distanse = Distan;
    });
  }
  AddOrder(Form: NgForm) {
    const body = {AddressStartPoint: $('#StartP1').val(), AddressEndPoint: $('#EndP1').val(), 
    TariffType: Form.value.Type, Distance: this.Distanse, PaymentTypeId: this.TypePayment};
    console.log(body);
    this.http.post('CabRide/AddOrder', body).subscribe(data => {
      alert('Заказ успешен');
    });
  }
}
