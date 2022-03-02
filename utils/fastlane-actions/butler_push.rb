module Fastlane
  module Actions
    module SharedValues
    end

    class ButlerPushAction < Action
      def self.run(params)

       data_path = params[:data_path]
       username = params[:username]
       project_id = params[:project_id]
       channel = params[:channel]

        cmd = ["butler",
          "push",
          "#{data_path}",
          "#{username}/#{project_id}:#{channel}"]

        UI.message "Butler push..."

        if system(*cmd)
          UI.success "Butler push finished."
        else
          UI.user_error! "Butler push failed."
        end

      end

      #####################################################
      # @!group Documentation
      #####################################################

      def self.description
        "Push with itch.io butler"
      end

      def self.details
        "You can use this action to upload a build with itch.io butler."
      end

      def self.available_options
        # Define all options your action supports. 
        
        # Below a few examples
        [
          FastlaneCore::ConfigItem.new(key: :data_path,
                                       env_name: "FL_BUTLER_PUSH_DATA_PATH", # The name of the environment variable
                                       description: "Data Path", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("Data path not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :username,
                                       env_name: "FL_BUTLER_PUSH_USERNAME", # The name of the environment variable
                                       description: "Username", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("Username not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :project_id,
                                       env_name: "FL_BUTLER_PUSH_PROJECT_ID", # The name of the environment variable
                                       description: "Project Id", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("Project id not defined.") unless (value and not value.empty?)
                                       end),
          FastlaneCore::ConfigItem.new(key: :channel,
                                       env_name: "FL_BUTLER_PUSH_CHANNEL", # The name of the environment variable
                                       description: "Channel", # a short description of this parameter
                                       verify_block: proc do |value|
                                          UI.user_error!("Channel not defined.") unless (value and not value.empty?)
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

        true
      end
    end
  end
end
