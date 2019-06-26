
import Vue from 'vue'
import App from './App'
import router from './router'
import Vuetify from 'vuetify'
import 'vuetify/dist/vuetify.min.css'
import {store} from './store'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Breabcrumbs from 'vue-2-breadcrumbs'
 /* eslint-disable no-new */
Vue.use(Breabcrumbs)
// Vuetify.com koriscen za dizajn
// Osposobljavanje Axios-a za HTTP zahteve i Vuetify za HTML/CSS programa (nalik na Google dizajn)
Vue.use(VueAxios, axios)
// vuetify postavljanje globalnih imena za boje
Vue.use(Vuetify, {
  theme: {
    navbarcolor: '#03996F',
    redactive: '#E4B056'
  }
})

Vue.config.productionTip = false

new Vue({
  el: '#app',
  // router sluzi za mogucnost prelaza sa strane na stranu koriscenjem v-router-a
  router,
  // Vue-X store sluzi za kreiranje frontend baze za rad sa signle page app
  store,
  components: { App },
  template: '<App/>',
  // created metoda se desava pri prvom pokretanju komponente u ovom slucaju programa
  created () {
    // pozovi sve inicijalne metode za popunjavanje store-a sa podacima neophodnim za funkcionisanje programe
    this.$store.dispatch('loadedUcenici')
    this.$store.dispatch('loadedSastanci')
    this.$store.dispatch('LoadedStatusiPrijave')
    this.$store.dispatch('loadedTipoviPorodice')
    this.$store.dispatch('loadedSSS')
    this.$store.dispatch('loadedDrzave')
    this.$store.dispatch('loadedPostanskiBrojevi')
    this.$store.dispatch('loadedPolovi')
    this.$store.dispatch('loadedSmerovi')
    this.$store.dispatch('loadedSS')
    this.$store.dispatch('loadedOS')
    this.$store.dispatch('loadedMesta')
    this.$store.dispatch('loadedRazred')
    this.$store.dispatch('loadedVaspitneGrupe')
    this.$store.dispatch('loadedPohvale')
    this.$store.dispatch('loadedKazne')
    this.$store.dispatch('loadedVaspitac')
    this.$store.dispatch('LoadedMeetups')
    this.$store.dispatch('loadedSobe')
    this.$store.dispatch('loadedGodisnjiProgram')
    return this.$store.dispatch('loadedOpstine')
  }
})
