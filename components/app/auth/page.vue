<script setup lang="ts">
import { signInWithEmailAndPassword } from 'firebase/auth'
import { f7List, f7ListButton, f7ListInput, f7LoginScreenTitle, f7NavLeft, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page } from 'framework7-vue'

const auth = useFirebaseAuth()

const state = reactive({
  email: '',
  password: '',
})

async function login() {
  signInWithEmailAndPassword(auth!, state.email, state.password)
}
</script>

<template>
  <f7Page login-screen no-toolbar no-navbar no-swipeback>
    <f7LoginScreenTitle>SSTAA Login</f7LoginScreenTitle>
    <f7List form>
      <f7ListInput
        v-model:value="state.email"
        label="Email"
        type="email"
        placeholder="Your email (example: aurelius@sst.edu.sg)"
      />
      <f7ListInput
        v-model:value="state.password"
        label="Password"
        type="password"
        placeholder="Your password"
        error-message="Nice"
      />
    </f7List>
    <f7List inset>
      <f7ListButton @click="login">
        Login
      </f7ListButton>
    </f7List>
  </f7Page>
</template>
