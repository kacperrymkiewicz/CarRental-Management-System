export const capitalizeMixin = {
  methods: {
    capitalizeFirstLetter: str => str.charAt(0).toUpperCase() + str.slice(1)
  }
};