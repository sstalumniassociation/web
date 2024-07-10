import antfu from '@antfu/eslint-config'

export default antfu(
  {
    stylistic: true, // enable stylistic formatting rules
    typescript: true,
    vue: true,
    jsonc: true,
    yaml: true,
  },
  {
    rules: {
      'vue/component-name-in-template-casing': ['error', 'PascalCase'],
      // https://github.com/antfu/eslint-config/pull/214
      'node/prefer-global/process': [
        'off',
        'never',
      ],
    },
  },
  {
    files: ['SSTAlumniAssociation.WebApp/api/**/*.ts'],
    rules: {
      'eslint-comments/no-unlimited-disable': ['off'],
    },
  },
)
