<template>
  <div class="fullscreen bg-grey-2 row items-center justify-evenly">
    <div class="column">
      <div class="text-center">
        <h5 class="q-my-md">Worker System</h5>
      </div>

      <div class="row">
        <!-- custom: open login page and remove local storage login data -->
        <q-card q-model="removeLogin" square bordered class="q-pa-lg shadow-1">
          <q-card-section>
            <q-form class="q-gutter-md">
              <div>
                <q-input bottom-slots v-model="account" label="Account">
                  <template v-slot:prepend>
                    <q-icon name="assignment_ind" />
                  </template>

                  <template v-slot:append>
                    <q-icon name="close" @click="account = ''" class="cursor-pointer" />
                  </template>
                </q-input>
              </div>

              <div>
                <q-input bottom-slots v-model="password" :type="isPwd ? 'password' : 'text'" label="Password">
                  <template v-slot:prepend>
                    <q-icon name="lock" />
                  </template>

                  <template v-slot:append>
                    <q-icon :name="isPwd ? 'visibility_off' : 'visibility'" class="cursor-pointer" @click="isPwd = !isPwd" />
                    <q-icon name="close" @click="password = ''" class="cursor-pointer" />
                  </template>
                </q-input>
                <!-- custom if true, then display -->
                <p v-if="passwordstatus">Password is error</p>
              </div>
            </q-form>
          </q-card-section>

          <q-card-actions>
            <q-btn rounded unelevated color="primary" size="lg" class="full-width" label="Login" :disable="!account || !password" @click="submit" />
          </q-card-actions>
        </q-card>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
// custom: import custom api
import { loginAPI } from 'boot/axios'

export default defineComponent({
  name: 'Login',
  // custom: default setting
  data () {
    return {
      account: ref(''),
      password: ref(''),
      isPwd: ref(true),
      passwordstatus: ref(false),
      // custom remove local storage login data
      removeLogin: [
        localStorage.removeItem('token'),
        localStorage.setItem('token', '')
      ]
    }
  },

  methods: {
    submit: async function () {
      // if ((this.account === 'test') && (this.password === '123')) {
      //   localStorage.setItem('token', 'true')
      //   localStorage.setItem('Display', 'Test')
      //   return this.$router.push({ name: 'home' })
      // } else if ((this.account === 'ryuk') && (this.password === '456')) {
      //   localStorage.setItem('token', 'true')
      //   localStorage.setItem('Display', 'Ryuk')
      //   return this.$router.push({ name: 'home' })
      // } else {
      //   this.password = ''
      //   this.passwordstatus = true
      // }

      // const loginParams = {
      //   account: this.account,
      //   password: this.password
      // }

      // custom: model
      interface AuthLogin {
        isAuth: boolean,
        display: string,
        first: number
      }

      let result: AuthLogin

      // custom api send request
      await loginAPI.post<AuthLogin>('/Login', null, {
        params: {
          account: this.account,
          password: this.password
        }
      })
        // custom: get response
        .then((response) => {
          console.log(response)

          result = response.data
          if (result.isAuth) {
            // custom: set local storage
            localStorage.setItem('token', JSON.stringify(result))
            return this.$router.push({ name: 'home' })
          } else {
            this.password = ''
            this.passwordstatus = true
          }
        })
        .catch((error) => {
          console.log(error)
        })
    }
  }
})
</script>
