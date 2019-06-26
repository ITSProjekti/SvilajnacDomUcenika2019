<template>
  <v-container>
    <v-layout row wrap>
      <v-flex xs12 sm10 md8 offset-sm1 offset-md2>
        <v-card class="navbarcolor" :v-for="sastanak in sastanci" :key="sastanci.id">
          <v-container fluid>
            <v-layout row>
              <v-flex xs5 sm4 md3>
                <v-card-media
                  src="https://cdn-images-1.medium.com/max/1600/1*MoJ3ZQZ0YW0bCjNz_6JxhQ.png"
                  height="130px"
                ></v-card-media>
              </v-flex>
              <v-flex xs7 sm8 md9>
                <v-card-title primary-title>
                  <div>
                    <h2 class="white--text mb-0">Sastanci</h2>
                    <div>23.05.2019.</div>
                  </div>
                    </v-card-title>
                <v-card-action>
                  <v-btn flat  @click="onLoadSastanak(s)">
                    <v-icon light>arrow_forward</v-icon>Pogledaj sastanak
                  </v-btn>
                </v-card-action>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>
export default {
  data: () => ({
      
  }),
  methods: {
  onLoadSastanak(s) {
   this.$router.push("/sastanak");
   },
 
  },
  computed:{
      sastanci() {
          return this.$store.getters.loadedSastanak
      }
  }
};
</script>
