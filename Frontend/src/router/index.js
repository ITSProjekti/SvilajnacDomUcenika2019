import Vue from 'vue'
import Router from 'vue-router'
import Home from '@/components/Home'
import NoviUcenik from '@/components/Ucenik/NoviUcenik'
import Ucenici from '@/components/Ucenik/Ucenici'
import Ucenik from '@/components/Ucenik/Ucenik'
import VaspitnaGrupa from '@/components/VaspitniRad/VaspitnaGrupa'
import VaspitneGrupe from '@/components/VaspitniRad/VaspitneGrupe'
import Vasitac from '@/components/VaspitniRad/Vaspitac'
import Profile from '@/components/User/Profile'
import Register from '@/components/User/Register'
import SignIn from '@/components/User/SignIn'
import Breabcrumbs from 'vue-2-breadcrumbs'
import NovaSoba from '@/components/Sobe/NovaSoba'
import SpisakSoba from '@/components/Sobe/SpisakSoba'
import Sastanci from '@/components/Sastanak/Sastanci'
import KreirajSastanak from '@/components/Sastanak/KreirajSastanak'
import NovaSekcija from '@/components/Sekcije/NovaSekcija'
import GodisnjiProgram from '@/components/Sastanak/GodisnjiProgram'
import Godisnji from '@/components/Sastanak/Godisnji'
import MesecniRad from '@/components/Sastanak/MesecniRad'
import Mesecni from '@/components/Sastanak/Mesecni'
import Evidencija from '@/components/Sastanak/Evidencija'
import PregledPrimljenihUcenika from '@/components/Sastanak/PregledPrimljenihUcenika'

 /* eslint-disable */
Vue.use(Breabcrumbs)

Vue.use(Router)
// podesavanja vue routera, komponente za navigaciju/rutiranje programa
export default new Router({
  routes: [
    {path:'/', component: Home, name:'',
    meta: {
      //pokusaj podesavanja breadcrumb-a, TO-DO
      breadcrumb: 'Pocetna'  
    }},
    {path:'/ucenici', component: Ucenici, name:'Ucenici',
    meta: {
      breadcrumb: 'Prijavljeni ucenici'  
    }},
    {path:'/prijava', component: NoviUcenik, name:'Prijava',
    meta: {
      breadcrumb: 'Prijava ucenika'  
    }},
    {path:'/detalji/:id', component: Ucenik},
    {path:'/vaspitaci', component: Vasitac},
   
    {path:'/vasptinagrupa', component: VaspitnaGrupa},
    {path:'/pregledUcenika/', component: PregledPrimljenihUcenika, name: 'PregledPrimljenihUcenika'},
    {path:'/vaspitnegrupe/', component: VaspitneGrupe},
    {path:'/sastanci/', component: Sastanci},
    {path:'/kreirajsastanak/', component: KreirajSastanak, name: 'KreirajSastanak'},
    {path:'/profil/', component: Profile},
    {path:'/registracija/', component: Register},
    {path:'/logovanje/', component: SignIn},
    {path:'/godisnjiprogram/', component: GodisnjiProgram, name: 'GodisnjiProgram'},
    {path:'/godisnji/', component: Godisnji, name: 'Godisnji'},
    {path:'/mesecnirad/', component: MesecniRad, name: 'MesecniRad'},
    {path:'/mesecni/', component: Mesecni, name: 'Mesecni'},
    {path:'/evidencija/', component: Evidencija, name: 'Evidencija'},

    {path:'/novasoba/', component: NovaSoba, name:'NovaSoba'},
    {path:'/spisaksoba/', component: SpisakSoba, name:'SpisakSoba'},
    

    {path:'/novasekcija/', component: NovaSekcija, name:'NovaSekcija'},
   
   ],
    mode:'history'
})

