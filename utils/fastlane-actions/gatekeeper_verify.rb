module Fastlane
  module Actions
    module SharedValues
    end

    class GatekeeperVerifyAction < Action
      def self.run(params)

        target = params[:target]

        UI.message "Verify security assessment..."
        sh("spctl","-vvv","--assess","--type","exec",target)
        
      end

      #####################################################
      # @!group Documentation
      #####################################################

      def self.description
        "Verify App with macOS Gatekeeper"
      end

      def self.details
        "You can use this action to verify your app with macOS Gatekeeper"
      end

      def self.available_options
        # Define all options your action supports.

        # Below a few examples
        [
          FastlaneCore::ConfigItem.new(key: :target,
                                       env_name: "FL_CODESIGN_TARGET", # The name of the environment variable
                                       description: "Target (eg. App Path)", # a short description of this parameter
                                       verify_block: proc do |value|
                                         UI.user_error!("Target not defined.") unless (value and not value.empty?)
          end)
        ]
      end

      def self.output
        []
      end

      def self.return_value
      end

      def self.authors
        ["Mario von Rickenbach"]
      end

      def self.is_supported?(platform)

        [:mac].include?(platform)
      end
    end
  end
end
