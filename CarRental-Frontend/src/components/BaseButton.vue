<template>
    <button :class="classes" class="base-button btn btn-primary">
        <span class="span-text">
            <slot></slot>
        </span>
        <span v-if="this.hasIcon" class="span-icon">
            <img src="@/assets/images/icons/svg/base_button_arrow_right.svg">
        </span>
    </button>
</template>


<script>
export default {
    props: {
        type: {
            type: String,
            required: true
        },
        hasIcon: {
            type: Boolean,
            required: false,
        }
    },
    computed: {
        classes() {
            return {
                'button-dark': this.type === 'dark',
                'button-light': this.type === 'light',
                'button-inaccessible' : this.type === 'inaccessible',
            };
        },
    },
}

</script>

<style lang="scss" scoped>
    button {
        padding: 10px 20px;
        font-weight: 500;
        margin: 30px 0;

        .span-icon {
            margin-left: 10px;
            display: inline-block;
            transition: all 0.2s ease-in-out;
        }

        &:hover .span-icon {
            transform: translateX(5px);
        }
    }

    .button-dark {
        background-color: $button-dark;
        border-color: $button-dark;

        &:hover, &:active {
            background-color: $button-dark-hover;
            border-color: $button-dark-hover;
        }
    }
    .button-light {
        background-color: $button-light;
        border-color: $button-light;
        color: $secondary;

        &:hover, &:active {
            background-color: $button-light-hover;
            border-color: $button-light-hover;
        }
    }

    .button-inaccessible {
        background-color: #ebebeb;
        border-color: #9ea2aa;
        color: $secondary;
        text-decoration: line-through;
        cursor: not-allowed;

        &:hover, &:active {
            background-color: #ebebeb;
            border-color: #9ea2aa;
        }
    }
</style>