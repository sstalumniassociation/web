<script setup lang="ts">
import { FetchError } from 'ofetch'
import { signInWithEmailAndPassword } from 'firebase/auth'
import { f7, f7List, f7ListButton, f7ListInput, f7LoginScreenTitle, f7NavLeft, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page } from 'framework7-vue'

const auth = useFirebaseAuth()

const state = reactive({
  email: '',
  password: '',
  accountLinked: null as boolean | null,
  pending: false,
  error: {} as Partial<{ email: string, password: string }>,
})

async function login() {
  try {
    state.pending = true
    const account = await $fetch('/api/auth/verify', {
      method: 'POST',
      body: {
        email: state.email,
      },
    })
    state.accountLinked = account.linked
  }
  catch (err) {
    if (err instanceof FetchError && err.statusCode === 404)
      f7.toast.show({
        text: 'Please register with SSTAA',
        closeTimeout: 3000,
      })
    else
      state.error.email = (err as Error).message
  } finally {
    state.pending = false
  }
}

async function validateEmail() {
  signInWithEmailAndPassword(auth!, state.email, state.password)
}
</script>

<template>
  <f7Page login-screen no-toolbar no-navbar no-swipeback>
    <f7LoginScreenTitle>SSTAA</f7LoginScreenTitle>
    <f7List form @submit.prevent="login">
      <f7ListInput v-model:value="state.email" label="Email" type="email" placeholder="Your email address" autofocus
        :error-message="state.error.email" :error-message-force="!!state.error.email" />
      <f7ListInput v-if="state.accountLinked" v-model:value="state.password" label="Password" type="password"
        placeholder="Your password" />
    </f7List>
    <f7List inset>
      <f7ListButton type="submit" @click="login">
        Login
      </f7ListButton>
    </f7List>
  </f7Page>
</template>
