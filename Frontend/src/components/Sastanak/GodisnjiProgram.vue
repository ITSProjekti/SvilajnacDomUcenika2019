<template>
  <v-container fluid>
           <v-layout row>
        <v-flex xs12  style="text-align: center">
          <transition name="slidetoleft" appear>
            <h1>Godišnji program</h1>
          </transition>
          <br><br>
        </v-flex>
    </v-layout>
    <v-form
                    ref="form"
                    v-model="valid"
                    lazy-validation
                    @submit.prevent="GodisnjiProgramRada"
                    >
    <v-layout row>
      <v-flex xs4 order-md1 order-xs1>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Programsko područje</v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs4 order-md2 order-xs1>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Učenje i razvijanje...</v-card-text>
        </v-card>
      </v-flex>
      <v-flex xs4 order-md3 order-xs1>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Razvijanje ličnosti i soc...</v-card-text>
        </v-card>
        
      </v-flex>
       <v-flex xs4 order-md4 order-xs1>
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Problematika slobodnog</v-card-text>
        </v-card>
        
      </v-flex>
    </v-layout>
    
    <v-layout row>
    <v-flex xs4 order-md1 order-xs1>
     <v-card tile flat style="background-color: rgb(245,245,245)">
          <v-card-text id="card-text-stil">Broj tema -></v-card-text>
        </v-card>
        </v-flex>

         <v-flex xs5 order-md2 order-xs1>
          <v-text-field
            label="Upišite broj"
             clearable
             :rules="[rules.upisBroja]"
             
          ></v-text-field>
        </v-flex>
         <v-flex xs5 order-md3 order-xs1>
          <v-text-field
            label="Upišite broj"
            clearable
             :rules="[rules.upisBroja]"
          ></v-text-field>
        </v-flex>
         <v-flex xs5 order-md4 order-xs1>
          <v-text-field
            label="Upišite broj"
            clearable
             :rules="[rules.upisBroja]"
          ></v-text-field>
        </v-flex>
 </v-layout>
    <v-layout row>
        <v-flex xs3 offset-xs style="">
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Programsko područje</v-card-text>
        </v-card>
        </v-flex>
        <v-flex xs3 offset-xs style="">
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Tema</v-card-text>
        </v-card>
        </v-flex>
        <v-flex xs3 offset-xs style="">
        <v-card dark tile flat color="navbarcolor">
          <v-card-text>Mesec</v-card-text>
        </v-card>
        </v-flex>
    </v-layout>
        <v-layout row>
            <v-flex xs3 offset-xs order-xs1>
            <v-text-field
             v-model="editModel.ProgramskoPodrucje"
                label="Upišite programsko područje"
                clearable
                required
                progress
              
              :rules="[rules.required, rules.obaveznaPolja]" 
            ></v-text-field>
            </v-flex>
            <v-flex xs3 offset-xs order-xs1>
            <v-select
            :items="items2"
            v-model="editModel.Tema"
                label="Izaberite temu"
                 clearable
              :rules="[rules.required, rules.obaveznaPolja]"
            ></v-select>
            </v-flex>
            <v-flex  xs3 offset-xs order-xs1>
                    <v-select
            :items="items"
            v-model="editModel.Mesec"
            label="Izaberite mesec"
            
            ></v-select>
                </v-flex>
            
        </v-layout>
        <v-layout>
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
      items2:['Učenje i razvijanje lične kompetentnosti učenika', 'Razvijanje ličnosti i socijalnog saznanja', 'Problematika slobodnog vremena'],
rules:{
       upisBroja: (value) => {
            const pattern = /^\d*$/
            return pattern.test(value) || 'Ovo polje mora sadržati brojeve.'
       },
        obaveznaPolja: (value) => {
            const pattern = /[a-zA-Z]\d*$/
            return pattern.test(value) || 'Ovo polje je obavezno.'
          }
},
 editModel:{
        id:'',
        ProgramskoPodrucje:'',
        Tema: '',
        Mesec:'',
    
    },
    created: function(){

       this.DugmeSubmit();
    }
    
    }),
    methods: {
        DugmeSubmit: function(){
        this.ProgramskoPodrucje = 'Loading...';
        var vm = this;
        axios.post('http://localhost:62768/api/GodisnjiProgramRada')
        .then(function(response){
            vm.ProgramskoPodrucje = response.data[0];
        })
        .catch(function(error){
            vm.ProgramskoPodrucje = 'Greska' + error;
        });
      
  },
GodisnjiProgramRada () {
  //  console.log(this.editedItem)
        this.$store.dispatch('createGodisnjiProgram',this.editModel)
        this.$router.push('/godisnji')
      }  
    
  },
  computed:{
      ProgramskoPodrucje () {
       return this.$store.getters.loadedProgramskoPodrucje      
      },
      Tema () {
       return this.$store.getters.loadedTema    
      },
      Mesec () {
       return this.$store.getters.loadedMesec   
      },
  }}
</script>

<style>
#card-text-stil{
    font-size: 18px;
    font-weight: 900;
    text-align: center;
}

</style>
