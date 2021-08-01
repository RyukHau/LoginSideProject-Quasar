<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar class="bg-teal-6" inverted>
        <q-btn
          flat
          dense
          round
          icon="list"
          aria-label="Menu"
          @click="toggleLeftDrawer"
        />

        <q-toolbar-title>
          <!-- custom: ":to" is route to page -->
          <div>
            <q-btn flat class="text-h6" :to="{ name: 'home' }">Work System</q-btn>
          </div>
          <q-toolbar-title class="text-subtitle1 text-weight-thin"></q-toolbar-title>
        </q-toolbar-title>

        <!-- custom: click is script method -->
        <div>
          <q-btn flat @click="con" :label="` ${DisplayName} `" />
          <q-btn flat dense icon="logout" @click="logout">Logout</q-btn>
        </div>
      </q-toolbar>
    </q-header>

    <!-- custom: show-if-above set to display menu list by default -->
    <q-drawer
      v-model="leftDrawerOpen"
      show-if-above
      bordered
      class="bg-blue-grey-7"
    >
      <q-list>
        <q-item-label
          header
          class="text-grey-3"
        >
          Menu
        </q-item-label>

        <q-item clickable tag="a" class="text-grey-5" :to="{ name: 'home'}">
          <q-item-section avatar>
              <q-icon name="home" />
          </q-item-section>
          <q-item-section>
            <q-item-label>Home</q-item-label>
          </q-item-section>
        </q-item>

        <q-expansion-item
        icon="list"
        label="List"
        class="text-grey-5"
        :header-class="{'text-grey-5': true}"
        >
        <!-- custom: ":inset-level=1" is shadow -->
          <q-item clickable tag="a" :inset-level="1" :to="{name: 'list.daily' }">
            <q-item-section>
              <q-item-label>Daily</q-item-label>
            </q-item-section>
          </q-item>

          <q-item clickable tag="a" :inset-level="1" :to="{name: 'list.monthly' }">
            <q-item-section>
              <q-item-label>Monthly</q-item-label>
            </q-item-section>
          </q-item>
        </q-expansion-item>
      </q-list>
    </q-drawer>

  <q-footer elevated class="bg-teal-6">
    <q-toolbar class="copyright">
      <div>copyright © Ryuk</div>
    </q-toolbar>
  </q-footer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'

export default defineComponent({
  name: 'MainLayout',

  // custome: open page to action
  data () {
    // custom: model setting
    interface AuthLogin {
        isAuth: boolean,
        display: string,
        first: number
      }
    // custom: localstorage.getitem get that store data
    // custom: json.parse .. as use json to parse with model
    const name = JSON.parse(localStorage.getItem('token') || '{}') as AuthLogin
    return {
      DisplayName: name.display
    }
  },

  // custom: action to display or hide menu list
  setup () {
    const leftDrawerOpen = ref(false)

    return {
      leftDrawerOpen,
      toggleLeftDrawer () {
        leftDrawerOpen.value = !leftDrawerOpen.value
      }
    }
  },

  // action
  methods: {
    logout () {
      // custom: remove item clear local storage item
      localStorage.removeItem('token')
      // custom: route to login page
      return this.$router.push({ name: 'login' })
    },
    con () {
      interface AuthLogin {
        isAuth: boolean,
        display: string,
        first: number
      }
      const cons = JSON.parse(localStorage.getItem('token') || '{}') as AuthLogin
      console.log(cons)
    }
  }
})
</script>
