<script setup lang="ts">
import { FetchError } from 'ofetch'
import { createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'firebase/auth'
import { f7 } from 'framework7-vue'
import { FirebaseError } from 'firebase/app'

const opened = defineModel<boolean>('opened')

const FIREBASE_ERROR_MESSAGES: Record<string, string> = {
  'auth/user-disabled': 'Your account has been temporarily disabled. Please try again later.',
  'auth/weak-password': 'Your password is too weak. Please use a stronger password',
  'auth/wrong-password': 'Your password is incorrect. Please try again.',
}

const auth = useFirebaseAuth()

const state = reactive({
  email: '',
  password: '',
  confirmPassword: '',
  accountLinked: null as boolean | null,
  accountId: '',
  pending: false,
})

watch(() => state.email, () => {
  if (state.accountLinked !== null)
    state.accountLinked = null
})

async function login() {
  state.pending = true
  if (state.accountLinked === null)
    await validateEmail()
  else if (state.accountLinked === true)
    await firebaseLogin()
  else if (state.accountLinked === false)
    await firebaseRegister()
  state.pending = false
}

async function validateEmail() {
  try {
    const account = await $api('/api/auth/verify', {
      method: 'POST',
      body: {
        email: state.email,
      },
    })
    state.accountLinked = account.linked
    state.accountId = account.id
  }
  catch (err) {
    if (err instanceof FetchError && err.statusCode === 404) {
      f7.toast.show({
        text: 'Please register with SSTAA',
        closeTimeout: 10000,
      })
    }
    else {
      f7.toast.show({
        text: (err as Error).message,
        closeTimeout: 10000,
      })
    }
  }
}

async function firebaseLogin() {
  try {
    await signInWithEmailAndPassword(auth!, state.email, state.password)
  }
  catch (err) {
    if (err instanceof FirebaseError) {
      handleFirebaseError(err)
    }
    else {
      f7.toast.show({
        text: (err as Error).message,
        closeTimeout: 10000,
      })
    }
  }
}

async function firebaseRegister() {
  if (state.password !== state.confirmPassword) {
    f7.toast.show({
      text: 'Your passwords do not match',
      closeTimeout: 10000,
    })
    return
  }

  try {
    await createUserWithEmailAndPassword(auth!, state.email, state.password)
    await $api(`/api/user/${state.accountId}`, {
      method: 'POST',
      body: {
        firebaseId: auth!.currentUser!.uid,
      },
    })
  }
  catch (err) {
    if (err instanceof FirebaseError) {
      handleFirebaseError(err)
    }
    else {
      f7.toast.show({
        text: (err as Error).message,
        closeTimeout: 10000,
      })
    }
  }
}

function handleFirebaseError(err: FirebaseError) {
  f7.toast.show({
    text: FIREBASE_ERROR_MESSAGES[err.code] ?? 'An unknown error occurred',
    closeTimeout: 10000,
  })
}
</script>

<template>
  <F7LoginScreen v-model:opened="opened">
    <F7Page login-screen>
      <F7LoginScreenTitle>Sign in to SSTAA</F7LoginScreenTitle>
      <F7List form @submit.prevent="login">
        <F7ListInput
          v-model:value="state.email" label="Email" type="email" placeholder="Your email address"
          validate required
        />
        <template v-if="state.accountLinked !== null">
          <F7ListInput
            v-model:value="state.password" validate label="Password" type="password" placeholder="Your password"
            required
          />
          <F7ListInput
            v-if="state.accountLinked === false" v-model:value="state.confirmPassword" validate
            label="Confirm password" type="password" placeholder="Confirm password" required
          />
        </template>

        <F7List inset>
          <F7Button type="submit" preloader :loading="state.pending" :disabled="state.pending">
            {{ !state.accountLinked ? 'Continue' : 'Login' }}
          </F7Button>
        </F7List>
      </F7List>
    </F7Page>
  </F7LoginScreen>
</template>
