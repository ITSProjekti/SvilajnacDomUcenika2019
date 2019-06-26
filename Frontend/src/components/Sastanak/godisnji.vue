
<template>
  <div id="maindiv">
   <v-card>
    
    <v-layout wrap > 
        <v-flex xs12  class="text-xs-center">
        <v-progress-circular
          indeterminate
          class="primary--text"
          :width="7"
          :size="150"
          v-if="loading"></v-progress-circular>
      </v-flex>
    </v-layout>
    <v-card>
          <transition name="slidetoleft" appear>
       <v-card-title wrap >
        <h3>Pregled godisnjeg programa </h3> </v-card-title>
          </transition>
      <v-layout wrap justify-end >
        <v-flex xs3 class="mb-2 mr-3">
        <v-text-field      
        v-model="search"
        append-icon="search"
        label="Pretraga"
        single-line
        hide-details
      ></v-text-field>     
      
        </v-flex>
      
        <v-btn dark class="navbarcolor mt-2 mr-4"  @click.native="reloadPage">
          <img class ="mr-3 " :src=rangiraj.srcmain>   Sortiraj
        </v-btn>  
        
      </v-layout>
   <transition name="fade" appear  mode="in-out">
        <v-flex xs12>
          <!-- Glavna tabela prikaza svih prijavljenih ucenika -->
    <v-data-table  
      :headers="headers"
      :items="godisnji"
      :search="search"
      :custom-filter="customFilter"
      class="elevation-1"   
     >
      <template slot="items" slot-scope="props" >
        <tr>
          <td class="text-xs-left priority-1"  >{{ props.item.id}}</td>
          <td class="text-xs-left priority-1">{{ props.item.ProgramskoPodrucje }}</td>
            <td class="text-xs-left priority-1">{{ props.item.Tema }}</td>
        <td class="text-xs-left priority-1">{{ props.item.Mesec }}</td> 
        <td class="justify-center layout px-0">
          <v-btn center icon class="mx-0"
           @click="deleteItem(props.item)">
              <img :src=kanta.srcmain>
          </v-btn>
             <v-btn center icon class="mx-0"
          v-bind:to="'/detalji/'+props.item.id">          
         <img :src=izmena.srcmain>
          </v-btn>
        </td>
         </tr>
      </template>
       <template slot="no-data">
      <v-alert :value="true" color="error" icon="warning">
        Nema programa. :(
      </v-alert> 
    </template>
    <template slot="pageText" slot-scope="{ pageStart, pageStop }">
         {{ pageStart }} - {{ pageStop }} od {{ godisnji.length}}
</template>
    <v-alert slot="no-results" :value="true" color="error" icon="warning">
        Vasa pretraga za "{{ search }}" nije pronasla rezultate.
      </v-alert>
    </v-data-table>
    </v-flex>
</transition >
    </v-card>
</v-card>
  </div>
</template>


<script>
/* eslint-disable */
import moment from 'moment'

  export default {
    data: () => ({
      // dialog je promenljiva koja sluzi za prikazivanje dijaloga pri menjanju ili prijavi ucenika
      dialog: false,
      select: '',
     
      /////////////////////////////////////////
      kanta: { srcmain: require('../../assets/KANTA2.png')},
      izmena: { srcmain: require('../../assets/EDIT.png')} ,
       rangiraj: { srcmain: require('../../assets/RangirajIkona.png')} ,
      // headeri sluze za generisanje polja koja se prikazuju u tabeli
      headers: [
        {
          
        text: 'Redni broj',  align: 'left', sortable: false,  value: 'id', width:'100%' ,class: 'priority-1'},
       {
        text: 'Programsko podrucje',  align: 'left', sortable: false,  value: 'ProgramskoPodrucje', width:'100%' ,class: 'priority-1'},
        { text: 'Tema',value: 'Tema' ,align: 'left',sortable:true, width:'100%',class: 'priority-1'},
        { text: 'Mesec',value: 'Mesec' ,align: 'left',sortable:true, width:'100%',class: 'priority-1'},
                { text: 'Opcije', value: 'opcije',align: 'center',sortable:false,width:'100%' }

      ],
      // pomocna promenljiva za generisanje podatka o datumu rodjenja
       search: '',
      
      custom: true,
      editedIndex: -1,
      
        editedItem: {
         id:'',
         ProgramskoPodrucje: '',
         Tema: '',
         Mesec: '',
      },
    // defaultItem je objekat koji je po strukturi identican editedItemu i sluzi za resetovanje podataka u editedItemu na prazne podatke
    // pri zavrsetku prijave ili izmene podataka o uceniku
      defaultItem: {
         id:'',
         ProgramskoPodrucje: '',
         Tema: '',
         Mesec: '',
      }
    }),
    // computed metode su metode koje se desavaju onda kada dodje do nekakvim promena stanja komponente, neki vid watcher-a
    computed: {
      // logika za racunanje progress bar-a kod jmbg, 105 je prva granica a drugi parametar u math.min funkciji sluzi za formiranje 13 podeoka 
      // na progress baru za 13 jmbg cifara, dalje se ovi rezultati koriste za prikaz promene boja na progress baru
     
      godisnji () {
       return this.$store.getters.loadedGodisnjiProgram      
      },
      
      loading () {
        return this.$store.getters.loading
      },      
     
    },
    // watch se koristi kad se radi sa dijalozima
    watch: {
      dialog (val) {

        val || this.close()
      }
    },
    methods: {
      testMetoda(){
        console.log("proba");
      },
        reloadPage(){
   console.log('yo')
   this.$store.dispatch('loadedGodisnjiProgram')
  },
       
      editItem (item) {
        this.editedIndex = this.godisnjiProgram.indexOf(item)
        this.editedItem = Object.assign({}, item)
        this.previousItem = Object.assign({}, item)
        this.dialog = true
      },
      deleteItem (item) {
        const index = this.godisnjiProgram.indexOf(item)
    // pitanje za potrvrdu o brisanju gde ako se odabere potvrdan odgovor vrsi se poziv HTTP delete-a i brisanje ucenika iz vue-x store-a sa splice
    // na mestu index i broj 1 predstavlja broj ucenika koji se brisu
        confirm('Da li ste sigurni da zelite da izbrisete ovu sobu?') && this.$store.dispatch('deleteGodisnjiProgram',item.id)
         && this.godisnjiProgram.splice(index,1)
      },
    // ako se odabere opcija close na dijalogu treba editeditem vratiti na pocetni sa praznim podacima
      close () {
        
         this.dialog = false
        setTimeout(() => {
           this.editedItem = Object.assign({}, this.defaultItem)
             this.file=''
             this.imagePreview=''
           this.datum=null
          this.editedIndex = -1
        }, 300)
        
      },
      // ako se odabere potvrdan odgovor na dijalogu treba uradilit POST ili PUT metod u zavisnosti da li se radi o prijavi ili izmeni ucenika
      save () {
        // na osnovu toga da li postoji editedindex proveravamo da li se radi o novom ili starom uceniku
        // ako postoji onda je stari ucenik i radi se PUT zahtev nakon kojeg se datum i editedItem postavljaju na pocetne vrednosti
        if (this.editedIndex > -1) {    
          this.$store.dispatch('editGodisnjiProgram',this.editedItem)
          this.editedItem = Object.assign({}, this.defaultItem)
        } else {
          // u suprotnom radi se o novom uceniku te se vrsi POST metod nakon kojeg se editedItem i datum postavljaju na pocetne vrednosti
          this.$store.dispatch('createGodisnjiProgram',this.editedItem)
          this.editedItem = Object.assign({}, this.defaultItem)
        }
        this.close()
      }  
      }

  }
</script>

<style >

.slidetoleft-enter{
  opacity: 0;
}

.slidetoleft-enter-active{
  animation: slidetoleft-in 1s ease-out forwards;
  transition: opacity 2s ease-out;
  
}

.slidetoleft-leave-active {
  animation: slidetoleft-out 1s ease-out forwards;
  transition: opacity 2s ease-out;
  opacity: 0;
}

#maindiv 
{
  vertical-align: top;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 2s ease-out;
}

.fade-enter, .fade-leave-to {
  opacity: 0;
}

/* Css koji iskljucuje na input poljima za brojeve HTML5 spinner za brojeve */

input[type="number"]::-webkit-outer-spin-button, 
input[type="number"]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}
input[type="number"] {
    -moz-appearance: textfield;
}

/* menanje inicijalnog izgleda tabele*/


table {
  border-top: 1px solid grey   !important;
  border-bottom: 1px solid grey  !important;
  table-layout: fixed; width: 100%;
}
tr:nth-child(even) {background-color: lightgrey;}
td:nth-child(odd) {
    white-space:nowrap;
  overflow:hidden;
  text-overflow:ellipsis;


}td:nth-child(even) {
    white-space:nowrap;
  overflow:hidden;
  text-overflow:ellipsis;
 

}
/* responsive tabele, uklanjanje kolona */
      @media screen and (max-width: 1225px) and (min-width: 1045px) {
        /* prioriteti prikaza polja u tabelama i kod za proveru istih na osnovu prikaza ekrana u pikselima*/
		.priority-5{
			display:none;
		}
    
	}
	
	@media screen and (max-width: 1045px) and (min-width: 835px) {
		.priority-5{
			display:none;
		}
		.priority-4{
			display:none;
		}
	}
	
	@media screen and (max-width: 565px) and (min-width: 300px) {
		.priority-5{
			display:none;
		}
		.priority-4{
			display:none;
		}
		.priority-3{
			display:none;
		}
	}
	
	@media screen and (max-width: 300px) {
		.priority-5{
			display:none;
		}
		.priority-4{
			display:none;
		}
		.priority-3{
			display:none;
		}
		.priority-2{
			display:none;
		}
	
	}


  .responsive {
    width: 100%;
    height: auto;
    
}
</style>
