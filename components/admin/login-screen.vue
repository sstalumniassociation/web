<script setup lang="ts">
import { FetchError } from 'ofetch'
import { createUserWithEmailAndPassword, signInWithEmailAndPassword } from 'firebase/auth'
import { FirebaseError } from 'firebase/app'

const opened = defineModel<boolean>('opened')

const toast = useToast()
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
    if (!(err instanceof FetchError)) {
      return toast.add({
        severity: 'error',
        summary: 'An error occurred',
        detail: (err as Error).message,
        life: 10000,
      })
    }

    if (err.statusCode === 404) {
      return toast.add({
        severity: 'error',
        summary: 'Please register with SSTAA',
        life: 10000,
      })
    }

    toast.add({
      severity: 'error',
      summary: 'An error occurred',
      detail: err.data,
      life: 10000,
    })
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
      toast.add({
        summary: (err as Error).message,
        life: 10000,
      })
    }
  }
}

async function firebaseRegister() {
  if (state.password !== state.confirmPassword) {
    toast.add({
      summary: 'Your passwords do not match',
      life: 10000,
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
      toast.add({
        summary: (err as Error).message,
        life: 10000,
      })
    }
  }
}

function handleFirebaseError(err: FirebaseError) {
  toast.add({
    summary: FIREBASE_ERROR_MESSAGES[err.code] ?? 'An unknown error occurred',
    life: 10000,
  })
}
</script>

<template>
  <Dialog v-model:visible="opened" modal header="Sign in to SSTAA" :closable="false" class="w-full md:w-md mx-4">
    <form class="flex items-start flex-col gap-4" @submit.prevent="login">
      <div class="flex flex-col gap-2 w-full">
        <label for="email">
          Email
        </label>
        <InputText id="email" v-model="state.email" autofocus type="email" placeholder="Your email address" required />
      </div>

      <template v-if="state.accountLinked !== null">
        <div class="flex flex-col gap-2 w-full">
          <label for="password">
            Password
          </label>
          <InputText id="password" v-model="state.password" type="password" placeholder="Your password" required />
        </div>

        <div v-if="state.accountLinked === false" class="flex flex-col gap-2 w-full">
          <label for="confirm-password">
            Confirm password
          </label>
          <InputText
            id="confirm-password" v-model="state.confirmPassword" type="password" placeholder="Confirm password"
            required
          />
        </div>
      </template>

      <Button type="submit" :loading="state.pending" class="ml-auto" @click="login">
        {{ !state.accountLinked ? 'Continue' : 'Login' }}
      </Button>
    </form>
  </Dialog>
</template>
