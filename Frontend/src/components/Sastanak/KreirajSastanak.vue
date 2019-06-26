<template>
<v-container>
    <v-layout row>
        <v-flex xs12 sm6 offset-sm3>
<h2 color="navbarcolor">Kreiraj novi sastanak</h2>
        </v-flex>
    </v-layout>
    <form @submit.prevent="KreirajSastanak">
       <v-layout row>
           <v-flex xs12 sm6 offset-sm3>
               <v-spacer></v-spacer>
                     <p>Datum sastanka</p>
                    </v-flex>
            </v-layout >
                 <v-layout row>
                      <v-flex xs12 sm6 offset-sm3>         
                            <v-date-picker
                            color="navbarcolor"
                            v-model="datum"
                            locale="sr-Latn"
                            ></v-date-picker>
                            <p>Novi: {{datum}}</p>
                          </v-flex>
                          </v-layout>
         <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-text-field
                color="navbarcolor"
                label="Broj prisutnih ucenika"
                v-model="editedItem.brojPrisutnihUcenika"
                >
                </v-text-field>
            </v-flex>
        </v-layout>
     <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-text-field
                color="navbarcolor"
                label="Ukupan broj prisutnih ucenika"
                v-model="editedItem.ukupanBrojPrisutnihUcenika"
                > 
                </v-text-field>
            </v-flex>
        </v-layout>
                              <v-layout row wrap>
                                    <v-flex xs12 sm6 offset-sm3>
                                        <v-select
                                          color="navbarcolor"
                                          :loading="loading"
                                          :items="VaspitneGrupe"
                                          v-model="editedItem.VaspitnaGrupa.id"
                                          label="Izaberite vaspitnu grupu"
                                          item-text="Naziv"
                                          item-value="id"
                                          autocomplete
                                          required
                                          @change="changedGroup"
                                          :rules="[rules.required]"
                                        ></v-select>
                                      </v-flex>
                                    </v-layout>
                                  
         <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-text-field
                color="navbarcolor"
                label="Opis dnevnog reda"
                multi-line
                v-model="editedItem.opisDnevnogReda"
                >
                    
                </v-text-field>
            </v-flex>
        </v-layout>
         <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-text-field
                color="navbarcolor"
                label="Odluke donesene na sastanku"
                multi-line
                v-model="editedItem.odlukeDoneseneNaSastanku"
                >
                </v-text-field>
            </v-flex>
        </v-layout>
         <v-layout row>
            <v-flex xs12 sm6 offset-sm3>
                <v-text-field
                color="navbarcolor"
                label="Zaključci sastanka"
                multi-line
                v-model="editedItem.zakljucciSastanka"
                >
                </v-text-field>
            </v-flex>
        </v-layout>
        <v-layout>
            <v-flex xs12 sm6 offset-sm3>
                <v-btn class="primary" type="submit" :disabled="!formaJeValidna">Kreiraj sastanak</v-btn>
            </v-flex>
        </v-layout>
    </form>
</v-container>
</template>

<script>
export default {
    data: () => ({
    rules: {
      // requiered pravilo je pravilo za neophodnost postojanja informacije koja se trazi na odgovarajucem polju sa opcijom requiered
          required: (value) => !!value || 'Ovo polje je obavezno.',
          name: (value) => {
            const pattern = /[a-zA-Z][^#&<>\"~;$^%{}?]{1,20}$/
            return pattern.test(value) || 'Ime ne sme sadržati brojeve, specijalne karaktere.'
          },
        },
        datum:null,
    editedItem:{
        id: '',
        datum: '',
        brojPrisutnihUcenika: '',
        ukupanBrojPrisutnihUcenika: '',
        opisDnevnogReda: '',
        odlukeDoneseneNaSastanku: '',
        zakljucciSastanka: '',
        VaspitnaGrupa: {
          id: '',
          Naziv: ''
        },
    }}),
    computed: {
        formaJeValidna () {
            if(this.datum == null){
                return false;
            }
            else{
                return true;
            }
        },
    
      VaspitneGrupe () {
        return this.$store.getters.loadedVaspitneGrupe
      },
},
methods:{
     KreirajSastanak (){
        this.formatiranjeDatuma()
        this.$store.dispatch('createSastanak',this.editedItem)
        this.$router.push('/sastanci')
    },
     loading () {
        return this.$store.getters.loading
      },
    formatiranjeDatuma()
      {
        console.log(this.datum)
          if(this.datum !== '')
          {
          const dan=this.datum.slice(-2); 
          this.editedItem.dan=dan
          const mesec=this.datum.substr(5,2)
          this.editedItem.mesec=mesec
          this.editedItem.godina=this.datum.substring(0,4)
          } 
      },
      changedGroup: function(value) {
     this.editedItem.VaspitnaGrupa.id=''
     this.editedItem.VaspitnaGrupa.Naziv=''
      },
}
}
</script>
