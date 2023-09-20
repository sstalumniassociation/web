<script>
import {
  f7AccordionContent,
  f7Block,
  f7BlockTitle,
  f7List,
  f7ListItem,
  f7Navbar,
  f7Page,
} from 'framework7-vue'

export default {
  components: {
    F7Navbar: f7Navbar,
    F7Page: f7Page,
    F7BlockTitle: f7BlockTitle,
    F7Block: f7Block,
    F7AccordionContent: f7AccordionContent,
    F7List: f7List,
    F7ListItem: f7ListItem,
  },
}
</script>

<template>
  <F7Page>
    <F7Navbar title="Accordion" back-link="Back" />

    <F7BlockTitle>List View Accordion</F7BlockTitle>
    <F7List strong outline-ios dividers-ios inset-md accordion-list>
      <F7ListItem accordion-item title="Lorem Ipsum">
        <F7AccordionContent>
          <F7Block>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean elementum id neque nec
              commodo. Sed vel justo at turpis laoreet pellentesque quis sed lorem. Integer semper
              arcu nibh, non mollis arcu tempor vel. Sed pharetra tortor vitae est rhoncus, vel
              congue dui sollicitudin. Donec eu arcu dignissim felis viverra blandit suscipit eget
              ipsum.
            </p>
          </F7Block>
        </F7AccordionContent>
      </F7ListItem>
      <F7ListItem accordion-item title="Nested List">
        <F7AccordionContent>
          <F7List>
            <F7ListItem title="Item 1" />
            <F7ListItem title="Item 2" />
            <F7ListItem title="Item 3" />
            <F7ListItem title="Item 4" />
          </F7List>
        </F7AccordionContent>
      </F7ListItem>
      <F7ListItem accordion-item title="Integer semper">
        <F7AccordionContent>
          <F7Block>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean elementum id neque nec
              commodo. Sed vel justo at turpis laoreet pellentesque quis sed lorem. Integer semper
              arcu nibh, non mollis arcu tempor vel. Sed pharetra tortor vitae est rhoncus, vel
              congue dui sollicitudin. Donec eu arcu dignissim felis viverra blandit suscipit eget
              ipsum.
            </p>
          </F7Block>
        </F7AccordionContent>
      </F7ListItem>
    </F7List>

    <F7BlockTitle>Opposite Side</F7BlockTitle>
    <F7List strong outline-ios dividers-ios inset-md accordion-list accordion-opposite>
      <F7ListItem accordion-item title="Lorem Ipsum">
        <F7AccordionContent>
          <F7Block>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean elementum id neque nec
              commodo. Sed vel justo at turpis laoreet pellentesque quis sed lorem. Integer semper
              arcu nibh, non mollis arcu tempor vel. Sed pharetra tortor vitae est rhoncus, vel
              congue dui sollicitudin. Donec eu arcu dignissim felis viverra blandit suscipit eget
              ipsum.
            </p>
          </F7Block>
        </F7AccordionContent>
      </F7ListItem>
      <F7ListItem accordion-item title="Nested List">
        <F7AccordionContent>
          <F7List>
            <F7ListItem title="Item 1" />
            <F7ListItem title="Item 2" />
            <F7ListItem title="Item 3" />
            <F7ListItem title="Item 4" />
          </F7List>
        </F7AccordionContent>
      </F7ListItem>
      <F7ListItem accordion-item title="Integer semper">
        <F7AccordionContent>
          <F7Block>
            <p>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean elementum id neque nec
              commodo. Sed vel justo at turpis laoreet pellentesque quis sed lorem. Integer semper
              arcu nibh, non mollis arcu tempor vel. Sed pharetra tortor vitae est rhoncus, vel
              congue dui sollicitudin. Donec eu arcu dignissim felis viverra blandit suscipit eget
              ipsum.
            </p>
          </F7Block>
        </F7AccordionContent>
      </F7ListItem>
    </F7List>
  </F7Page>
</template>
