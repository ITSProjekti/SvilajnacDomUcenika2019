<template>
  <v-container fluid grid-list-xl>
       <v-layout row>
        <v-flex xs12 style="text-align: center">
          <transition name="slidetoleft" appear>
            <h1>Mesečni plan i program</h1>
          </transition>
          <br><br>
        </v-flex>
    </v-layout>
    <v-form 
    ref="form"
    v-model="valid"
    lazy-validation
    @submit.prevent="MesecniPlanRada"
    >
    <v-layout wrap align-center>
      <v-flex xs4 offset-xs4>
        <v-select
          :items="items"
          v-model="editModel.Mesec"
          label="Izaberite mesec"
               :rules="[rules.required, rules.obaveznaPolja]"
        ></v-select>
      </v-flex>
     
    </v-layout>
     <v-layout row>
             <v-flex xs3 offset-xs2>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Sadržaj programa vaspitong rada</v-card-text>
        </v-card>
         <v-text-field
              v-model="editModel.SadrzajProgramaVaspitnogRada"
            label="Upišite sadržaj..."
     :rules="[rules.required, rules.obaveznaPolja]"
          ></v-text-field>
      </v-flex>
      <v-flex xs3 offset-xs2>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Metode i oblici vaspitnog rada</v-card-text>
        </v-card>
         <v-text-field
         :items="items"
          v-model="editModel.MetodeIObliciVaspitnogRada"
            label="Upišite metode..."
          :rules="[rules.required, rules.obaveznaPolja]"
          ></v-text-field>
      </v-flex>
    </v-layout>
    <v-layout row>
<v-flex xs12 sm6 offset-sm3>
                <v-text-field
                   :items="items"
          v-model="editModel.Napomena"
                color="navbarcolor"
                label="Napomena"
                multi-line
                >
                </v-text-field>
            </v-flex>
    </v-layout>
    <v-layout row>
<v-flex xs12 sm6 offset-sm3>
                <v-text-field
                   :items="items"
          v-model="editModel.Literatura"
                color="navbarcolor"
                label="Literatura"
                multi-line
                >
                </v-text-field>
            </v-flex>
    </v-layout>
    <v-layout row>
      <v-flex xs12 sm6 offset-sm5>
    <v-btn
    type="submit"
    >Potvrdi</v-btn> <!-- dugme treba da prosledi podatke u bazu / bice dodato naknadno -->
      </v-flex>
    </v-layout>
    </v-form>
  </v-container>
</template>


<script>
  export default {
    data: () => ({
      items: ['Januar', 'Februar', 'Mart', 'April', 'Maj', 'Jun', 'Jul', 'Avgust', 'Septembar', 'Oktobar', 'Novembar','Decembar'],
         rules:{
        obaveznaPolja: (value) => {
            const pattern = /[a-zA-Z]\d*$/
            return pattern.test(value) || 'Ovo polje je obavezno.'
          }
},
editModel:{
        id:'',
    Mesec:'',
    SadrzajProgramaVaspitnogRada:'',
    MetodeIObliciVaspitnogRada:'',
    Napomena:'',
    Literatura:''
    
    },
    created: function(){

       this.DugmeSubmit();
    }
    
    }),
    computed: {
      formIsValid () {      
        if( this.editedItem.Mesec !== '' &&
          this.editedItem.SadrzajProgramaVaspitnogRada!== '' &&         
          this.editedItem.MetodeIObliciVaspitnogRada !== ''
          )
          {
            return true
            }
          else{
            return false
          }
         
      }, 
    
     
    },
    methods: {
        DugmeSubmit: function(){
        this.Mesec = 'Loading...';
        var vm = this;
        axios.post('http://192.168.1.44:62768/api/MesecniPlanRada')
        .then(function(response){
            vm.Mesec = response.data[0];
        })
        .catch(function(error){
            vm.Mesec = 'Greska' + error;
        });
      
  },
MesecniPlanRada () {
  //  console.log(this.editedItem)
        this.$store.dispatch('createMesecni',this.editModel)
        this.$router.push('/mesecni')
      }  
    
  },
    
 
  }
</script>

<style>

</style>